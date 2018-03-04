using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Msr.Models.Orders;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;
using Msr.Services.Orders;
using Msr.Services.Orders.ViewModels;
using Msr.Services.Parts;
using Msr.Services.Procedures;
using Msr.Services.PrePro;
using Msr.Services.S3;
using Msr.Web.Controllers;
using Msr.Services.Files;
using Msr.Services.Files.ViewModels;

namespace Answer.Web.Controllers
{
    public class DocController : BaseController
    {
        private readonly FileService _fileService;
        private readonly AWSFileHandler _cloudUploader;
        public DocController()
        {
            _fileService = new FileService();
            _cloudUploader = new AWSFileHandler();
        }

        public ActionResult View(string filePath, string fileType, string fileName, int? height)
        {
            var orderService = new OrderService();
            var img = orderService.GetDocumentBase64(filePath, height);

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
            var orderService = new OrderService();

            foreach (HttpPostedFileBase file in files)
            {
                var imageModel = new SaveWorkItemImageViewModel();

                imageModel.Name = file.FileName;
                imageModel.Desc = null;

                var _cloudUploader = new AWSFileHandler();

                var keyName = string.Format("Answer2/{0}-{1}", Guid.NewGuid(), file.FileName);
                var buketName = ConfigurationManager.AppSettings.Get("AWSBuketName");

                _cloudUploader.UploadToCloud(file, buketName, keyName);

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
                imageModel.TaskId = taskId;

                orderService.SaveOrderItemImages(imageModel);
            }

            var orderItemImages = orderService.GetOrderItemImagesById(taskId);

            var images = new UploadedImageView();

            var imagesList = images.MapToDto(orderItemImages);

            return Json(new { files = imagesList.ToArray() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetImages(int fillId, int taskId)
        {
            var viewModel = new ImageViewModel();
            viewModel.FillId = fillId;
            viewModel.TaskId = taskId;

            return PartialView("_Images", viewModel);
        }

        public JsonResult GetImagesById(string taskId)
        {
            var orderService = new OrderService();

            var images = orderService.GetOrderItemImagesById(taskId);

            var imageView = new UploadedImageView();

            var imagesList = imageView.MapToDto(images);

            return Json(new { files = imagesList.ToArray() }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeleteImageById(string id, string taskId)
        {
            var orderService = new OrderService();

            var response = orderService.DeleteOrderItemImageById(id, GetCurrentUser().Id);

            if (response)
            {
                var orderItemImages = orderService.GetOrderItemImagesById(taskId);

                return Json(new { Message = "Image deleted successfully.", files = orderItemImages.ToArray() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Message = "Image upload failed." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFileView(string callBackId, string type)
        {
            ViewBag.CallBackId = callBackId;

            var docService = new DocumentFilesService();

            var resultsFiles = docService.GetSelectedRefFiles(callBackId, type, GetCurrentUser().Id).AsQueryable();

            return PartialView("_DocView", resultsFiles);
        }


        public ActionResult GetFileResult(string callBackId, string itemId, string type)
        {
            ViewBag.CallBackId = callBackId;

            var docService = new DocumentFilesService();

            var resultsFile = docService.GetSelectedRefFile(itemId);

            return Json(resultsFile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFileProcedureView(string callBackId)
        {
            ViewBag.CallBackId = callBackId;

            var docService = new DocumentFilesService();

            var resultsFiles = docService.GetProcedureSelectedRefFiles(callBackId, GetCurrentUser().Id).AsQueryable();

            return PartialView("_DocView", resultsFiles);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FileUploaderBootstrap(string objectId)
        {


            var fileService = new FileService();
            var documentService = new DocumentService();
            List<NewFile> selectedfiles = new List<NewFile>();

            var initialPreview = new List<string>();
            var initialPreviewConfigs = new List<DocLink>();

            var initialPreviewConfig = new object();
            var responese = new NewFile();
            foreach (string item in Request.Files)
            {
                var file = Request.Files[item];
                var imageModel = new SaveFileUploadViewModel();

                imageModel.Name = file.FileName;

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
                imageModel.Desc = null;
                imageModel.DropSrc = "YES";
                imageModel.NTLogin = GetCurrentUser().Id;

                responese = fileService.SaveFileUpload(imageModel);

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
                    type = x.TYPE,
                    size = 6666,
                    url = Url.Action("DeletesingleReference", "Documents", new { linkDocId = x.LINKED_DOC_ID }),
                    downloadUrl = cloudUrl,
                    key = x.LINKED_DOC_ID
                }).ToArray();

                if (responese.Status)
                {
                    selectedfiles.Add(responese);
                }
                if (objectId != null)
                    documentService.SaveSingleFileReference(objectId, responese.Id, GetCurrentUser().Id);
            }
            return Json(new { initialPreview, initialPreviewConfig, responese.Id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RefillUploader(string ids)
        {
            var documentService = new DocumentService();
            var array = ids.Split(',');
            array = array.Where(val => val != "0").ToArray();

            var initialPreviewConfigs = array.Select(id => documentService.GetListFileReferences(string.IsNullOrWhiteSpace(id) ? "0" : id)).ToList();

            foreach (var itemConfig in initialPreviewConfigs)
            {
                itemConfig.TYPE = itemConfig.CONTENTTYPE;
            }

            var initialPreview = initialPreviewConfigs.Select(itemConfig => itemConfig.SERVER_PATH).ToList();


            object initialPreviewConfig = initialPreviewConfigs.Select(x => new
            {
                caption = x.NAME,
                type = x.TYPE,
                size = 6666,
                url = Url.Action("DeletesingleReference", "Documents", new { linkDocId = x.LINKED_DOC_ID }),
                downloadUrl = x.SERVER_PATH,
                key = x.LINKED_DOC_ID
            }).ToArray();

            return Json(new { initialPreview, initialPreviewConfig }, JsonRequestBehavior.AllowGet);
        }
    }

}