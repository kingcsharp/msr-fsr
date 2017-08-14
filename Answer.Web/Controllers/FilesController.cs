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
using Msr.Models.Orders;
using Msr.Services.Files.ViewModels;
using Msr.Services.S3;

namespace Answer.Web.Controllers
{
    public class FilesController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Files";

            return View(viewModel);
        }

        public ActionResult GetFiles(string callBackId)
        {
            ViewBag.CallBackId = callBackId;

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
        public ActionResult Add(List<HttpPostedFileBase> files, string taskId)
        {
            var fileService = new FileService();

            foreach (HttpPostedFileBase file in files)
            {
                var imageModel = new SaveFileUploadViewModel();

                imageModel.Name = file.FileName;
                imageModel.Desc = null;

                var cloudUploader = new AWSFileHandler();

                var keyName = $"Answer2/{Guid.NewGuid()}-{file.FileName}";

                var buketName = ConfigurationManager.AppSettings.Get("AWSBuketName");

                cloudUploader.UploadToCloud(file, buketName, keyName);

                var baseUrl = ConfigurationManager.AppSettings.Get("AWSURL");

                var cloudUrl = $"{baseUrl}{keyName}";

                imageModel.Path = cloudUrl;
                imageModel.ContentType = file.ContentType;
                imageModel.SrcId = null;
                imageModel.SrcName = null;
                imageModel.SrcDesc = null;
                imageModel.SrcPath = null;
                imageModel.SrcContentType = null;
                imageModel.SrcChanged = null;
                imageModel.DocChanged = null;
                imageModel.DropSrc = "YES";
                imageModel.NTLogin = "1618";

                fileService.SaveFileUpload(imageModel);
            }
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewFile(string callBackitem)
        {
            var callBackUrl = "http://docs.google.com/gview?url=" + callBackitem + "&embedded=true";//callBackitem url need to be dynamic
            ViewBag.callBackitem = callBackUrl;

            return PartialView("_ViewFile");
        }
    }
}