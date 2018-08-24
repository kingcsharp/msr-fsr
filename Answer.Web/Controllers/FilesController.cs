using Msr.Models.Files;
using Msr.Services.Files;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Services.Documents;
using Msr.Services.Files.ViewModels;
using Msr.Services.S3;

namespace Answer.Web.Controllers
{
    [AuthorizeUser]
    public class FilesController : BaseController
    {
        private readonly FileService _fileService;

        private readonly DocumentFilesService _documentFilesService;
        private readonly string _awsBaseUrl;

        public FilesController()
        {
            _awsBaseUrl = ConfigurationManager.AppSettings.Get("AWSURL");
            _fileService = new FileService();
            _documentFilesService = new DocumentFilesService();
        }
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Files";

            return View(viewModel);
        }

        public ActionResult GetFiles(string callBackId, string targetCallBackId, string targetSection, string targetUplaodUrl)
        {
            ViewBag.CallBackId = callBackId;
            ViewBag.TargetCallBackId = targetCallBackId;
            ViewBag.TargetSection = targetSection;
            ViewBag.TargetUplaodUrl = targetUplaodUrl;
            return PartialView("_Files");
        }

        public ActionResult GetFile(string callBackId)
        {
            ViewBag.CallBackID = callBackId;

            return PartialView("_File");
        }

        public ActionResult FilesData(JqGridParam param)
        {
            var fileService = new FileService();

            var totalRows = fileService.GetFilesQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(FileView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data);
                    }
                    else if (rule.field == nameof(FileView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(FileView.Description))
                    {
                        totalRows = totalRows.Where(x => x.Description.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            string orderBy = param.sortColumn;

            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                orderBy = param.sortColumn;
            }

            if (param.sortOrder == "desc")
            {
                totalRows = totalRows.OrderByDescending(orderBy);
            }
            else
            {
                totalRows = totalRows.OrderBy(orderBy);
            }

            var totalRecords = totalRows.Count();
            totalRows = totalRows.Skip(param.pageSize * (param.pageIndex - 1));
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);

            var results = totalRows.ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Add(List<HttpPostedFileBase> files, string taskId, string[] title)
        {
            var fileService = new FileService();

            List<NewFile> selectedfiles = new List<NewFile>();

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                var imageModel = new SaveFileUploadViewModel();

                imageModel.Desc = title[i];

                imageModel.Name = file.FileName;

                var cloudUploader = new AWSFileHandler();

                var keyName = $"Answer2/{Guid.NewGuid()}-{file.FileName}";

                var buketName = ConfigurationManager.AppSettings.Get("AWSBuketName");

                cloudUploader.UploadToCloud(file, buketName, keyName);

                var baseUrl = ConfigurationManager.AppSettings.Get("AWSURL");

                var cloudUrl = $"{baseUrl}{keyName}";

                imageModel.Path = cloudUrl;
                imageModel.ContentType = file.ContentType;
                imageModel.DropSrc = "YES";
                imageModel.NTLogin = GetCurrentUser().Id;
                imageModel.FileUrl = cloudUrl;
                imageModel.FileKey = keyName;

                var responese = fileService.SaveFileUpload(imageModel);

                if (responese.Status == true)
                {
                    selectedfiles.Add(responese);
                }
            }
            return Json(selectedfiles, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ViewFile(string id)
        {
            try
            {
                var resultsFile = _documentFilesService.GetSelectedRefFile(id);

                var cloudUrl = $"{_awsBaseUrl}{resultsFile.FileKey}";

                var data = new
                {
                    FileUrl = cloudUrl,
                    FileName = resultsFile.Name
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(string id)
        {
            var imageModel = new SaveFileUploadViewModel();

            var taskService = _fileService.GetFilesQueryable().SingleOrDefault(x => x.Id == id);
            imageModel = imageModel.MapToDto(taskService);

            return View(imageModel);
        }
    }
}