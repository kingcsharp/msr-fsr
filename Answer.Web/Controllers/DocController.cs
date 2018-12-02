using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Commons.Files;
using Msr.Models.Menus;
using Msr.Models.Orders;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.Orders;
using Msr.Services.Orders.ViewModels;
using Msr.Services.S3;
using Msr.Services.Files;
using Msr.Services.Files.ViewModels;
using Msr.Services.PrePro;

namespace Answer.Web.Controllers
{
    [AuthorizeUser]
    public class DocController : BaseController
    {
        private readonly FileService _fileService;
        private readonly AWSFileHandler _cloudUploader;
        private readonly DocumentFilesService _documentFilesService;
        private readonly DocumentService _documentService;
        private readonly OrderService _orderService;
        private readonly PreProServices _preProServices;
        private readonly string _bucketName;
        private readonly string _awsBaseUrl;

        public DocController()
        {
            _fileService = new FileService();
            _cloudUploader = new AWSFileHandler();
            _documentFilesService = new DocumentFilesService();
            _documentService = new DocumentService();
            _orderService = new OrderService();
            _preProServices = new PreProServices();
            _bucketName = ConfigurationManager.AppSettings.Get("AWSBuketName");
            _awsBaseUrl = ConfigurationManager.AppSettings.Get("AWSURL");
        }

        public ActionResult View(string filePath, string fileType, string fileName, int? height)
        {
            var img = _orderService.GetDocumentBase64(filePath, height);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(Convert.FromBase64String(img), fileType);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FileUploader(List<HttpPostedFileBase> files, string taskId)
        {
            var getCurrentUser = GetCurrentUser();
            foreach (var file in files)
            {
                var keyName = $"Answer2/{Guid.NewGuid()}-{file.FileName}";

                _cloudUploader.UploadToCloud(file, _bucketName, keyName);

                var cloudUrl = $"{_awsBaseUrl}{keyName}";
                var imageModel = new SaveWorkItemImageViewModel
                {
                    Name = file.FileName,
                    Path = cloudUrl,
                    ContentType = file.ContentType,
                    DropSrc = "YES",
                    NTLogin = getCurrentUser.Id,
                    TaskId = taskId,
                    FileUrl = cloudUrl,
                    FileKey = keyName
                };


                _orderService.SaveOrderItemImages(imageModel);
            }

            var orderItemImages = _orderService.GetOrderItemImagesById(taskId);

            var images = new UploadedImageView();

            var imagesList = images.MapToDto(orderItemImages);

            return Json(new { files = imagesList.ToArray() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetImages(int fillId, int taskId)
        {
            var viewModel = new ImageViewModel
            {
                FillId = fillId,
                TaskId = taskId
            };

            return PartialView("_Images", viewModel);
        }

        public JsonResult GetImagesById(string taskId)
        {
            var images = _orderService.GetOrderItemImagesById(taskId);

            var imageView = new UploadedImageView();

            var imagesList = imageView.MapToDto(images);

            return Json(new { files = imagesList.ToArray() }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteImageById(string id, string taskId)
        {
            var result = _orderService.DeleteOrderItemImageById(id, GetCurrentUser().Id);

            return Json(result ? "Ok" : "error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFileView(string callBackId, string type)
        {
            ViewBag.CallBackId = callBackId;

            var resultsFiles = _documentFilesService.GetSelectedRefFiles(callBackId, type, GetCurrentUser().Id).AsQueryable();

            return PartialView("_DocView", resultsFiles);
        }

        public ActionResult GetFileResult(string callBackId, string itemId, string type)
        {
            ViewBag.CallBackId = callBackId;

            var resultsFile = _documentFilesService.GetSelectedRefFile(itemId);

            return Json(resultsFile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFileProcedureView(string callBackId)
        {
            ViewBag.CallBackId = callBackId;

            var resultsFiles = _documentFilesService.GetProcedureSelectedRefFiles(callBackId, GetCurrentUser().Id).AsQueryable();

            return PartialView("_DocView", resultsFiles);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FileUploaderBootstrap(string objectId)
        {
            var currentUser = GetCurrentUser();

            var initialPreview = new List<string>();
            var initialPreviewConfigs = new List<DocLink>();
            var initialPreviewConfig = new object();
            var responese = new NewFile();

            foreach (string item in Request.Files)
            {
                var file = Request.Files[item];
                var keyName = $"Answer2/{Guid.NewGuid()}-{file.FileName}";

                _cloudUploader.UploadToCloud(file, _bucketName, keyName);

                var cloudUrl = $"{_awsBaseUrl}{keyName}";

                var imageModel = new SaveFileUploadViewModel
                {
                    Name = file.FileName,
                    Path = cloudUrl,
                    ContentType = file.ContentType,
                    DropSrc = "YES",
                    NTLogin = currentUser.Id,
                    FileUrl = cloudUrl,
                    FileKey = keyName
                };

                responese = _fileService.SaveFileUpload(imageModel);

                initialPreview.Add(imageModel.Path);
                var docfiLink = new DocLink
                {
                    NAME = keyName,
                    CONTENTTYPE = file.ContentType,
                    LINKED_DOC_ID = responese.Id,
                    TYPE = file.ContentType
                };
                initialPreviewConfigs.Add(docfiLink);

                initialPreviewConfig = initialPreviewConfigs.Select(x => new
                {
                    caption = x.NAME,
                    type = MimeTypes.GetContentType(x.CONTENTTYPE),
                    size = 6666,
                    url = Url.Action("DeletesingleReference", "Documents", new { file = x.LINKED_DOC_ID }),
                    downloadUrl = cloudUrl,
                    key = x.LINKED_DOC_ID
                }).ToArray();

                if (objectId != null)
                {
                    _documentService.SaveSingleFileReference(objectId, responese.Id, currentUser.Id);
                }
            }
            return Json(new { initialPreview, initialPreviewConfig, responese.Id }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FileUploaderForWipTask(string objectId)
        {
            var currentUser = GetCurrentUser();

            var taskId = objectId.Split(',');

            var initialPreview = new List<string>();


            var initialPreviewConfigs = new List<DocLink>();

            var initialPreviewConfig = new object();
            string newId = "";

            foreach (string item in Request.Files)
            {
                var file = Request.Files[item];

                var keyName = $"Answer2/{Guid.NewGuid()}-{file.FileName}";

                _cloudUploader.UploadToCloud(file, _bucketName, keyName);

                var cloudUrl = $"{_awsBaseUrl}{keyName}";

                var imageModel = new SaveWorkItemImageViewModel
                {
                    Name = file.FileName,
                    Path = cloudUrl,
                    ContentType = file.ContentType,
                    DropSrc = "YES",
                    NTLogin = currentUser.Id,
                    TaskId = objectId,
                    FileUrl = cloudUrl,
                    FileKey = keyName
                };

                newId = _orderService.SaveOrderItemImages(imageModel);

                initialPreview.Add(imageModel.Path);

                var docfiLink = new DocLink
                {
                    NAME = keyName,
                    CONTENTTYPE = file.ContentType,
                    LINKED_DOC_ID = newId,
                    TYPE = file.ContentType
                };

                initialPreviewConfigs.Add(docfiLink);

                initialPreviewConfig = initialPreviewConfigs.Select(x => new
                {
                    caption = x.NAME,
                    type = MimeTypes.GetContentType(x.CONTENTTYPE),
                    size = 6666,
                    url = "/doc/DeleteImageById?Id=" + newId + "&taskId=" + taskId[0],
                    downloadUrl = cloudUrl,
                    key = x.LINKED_DOC_ID
                }).ToArray();

                Thread.Sleep(2000);

            }

            return Json(new { initialPreview, initialPreviewConfig, newId }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FileUploaderForPrePro(string objectId)
        {
            var currentUser = GetCurrentUser();

            var initialPreview = new List<string>();
            var initialPreviewConfigs = new List<DocLink>();

            var initialPreviewConfig = new object();

            NewFile responese = null;
            foreach (string item in Request.Files)
            {
                var file = Request.Files[item];

                var keyName = $"Answer2/{Guid.NewGuid()}-{file.FileName}";

                _cloudUploader.UploadToCloud(file, _bucketName, keyName);

                var cloudUrl = $"{_awsBaseUrl}{keyName}";

                var imageModel = new SaveFileUploadViewModel
                {
                    Name = file.FileName,
                    Path = cloudUrl,
                    ContentType = file.ContentType,
                    DropSrc = "YES",
                    NTLogin = currentUser.Id,
                    FileUrl = cloudUrl,
                    FileKey = keyName
                };

                responese = _fileService.SaveFileUpload(imageModel);

                initialPreview.Add(imageModel.Path);

                var docfiLink = new DocLink
                {
                    NAME = keyName,
                    CONTENTTYPE = file.ContentType,
                    LINKED_DOC_ID = responese.Id,
                    TYPE = file.ContentType
                };

                initialPreviewConfigs.Add(docfiLink);

                initialPreviewConfig = initialPreviewConfigs.Select(x => new
                {
                    caption = x.NAME,
                    type = MimeTypes.GetContentType(x.CONTENTTYPE),
                    size = 6666,
                    url = "/doc/DeletePreProImageById?Id=" + responese.Id + "&fileId=" + objectId,
                    downloadUrl = cloudUrl,
                    key = x.LINKED_DOC_ID
                }).ToArray();
                if (objectId != null)
                {
                    _preProServices.SavePreProSingleFileReference(objectId, responese.Id, currentUser.Id);
                }
            }

            return Json(new { initialPreview, initialPreviewConfig, responese?.Id }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeletePreProImageById(string id, string fileId)
        {
            var result = _preProServices.DeletePreProRefLinkImageById(id, fileId);

            return Json(result ? "Ok" : "error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RefillUploader(string ids)
        {
            var array = ids.Split(',');
            array = array.Where(val => val != "0").ToArray();

            var initialPreviewConfigs = array.Select(id => _documentService.GetListFileReferences(string.IsNullOrWhiteSpace(id) ? "0" : id)).ToList();

            foreach (var itemConfig in initialPreviewConfigs)
            {
                itemConfig.TYPE = itemConfig.CONTENTTYPE;
            }

            var initialPreview = initialPreviewConfigs.Select(itemConfig => itemConfig.SERVER_PATH).ToList();


            object initialPreviewConfig = initialPreviewConfigs.Select(x => new
            {
                caption = x.NAME,
                type = MimeTypes.GetContentType(x.CONTENTTYPE),
                size = 6666,
                url = Url.Action("DeletesingleReference", "Documents", new { file = x.LINKED_DOC_ID }),
                downloadUrl = x.SERVER_PATH,
                key = x.LINKED_DOC_ID
            }).ToArray();

            return Json(new { initialPreview, initialPreviewConfig }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download(string id)
        {
            try
            {
                var resultsFile = _documentFilesService.GetSelectedRefFile(id);

                var streamCloud = _cloudUploader.DownloadFromCloud(_bucketName, resultsFile.FileKey);

                return File(streamCloud, resultsFile.ContentType, resultsFile.Show);
            }
            catch (IndexOutOfRangeException)
            {
                AddErrorNotification($"Sorry. The document could not be found. Please contact support.");

                return View("Error");
            }
            catch (Exception)
            {
                AddErrorNotification("There is an error with download");

                return View("Error");
            }
        }
    }

}