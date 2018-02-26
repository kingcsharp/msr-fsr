using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Orders;
using Msr.Services.Documents;
using Msr.Services.Orders;
using Msr.Services.Orders.ViewModels;
using Msr.Services.Parts;
using Msr.Services.Procedures;
using Msr.Services.PrePro;
using Msr.Services.S3;
using Msr.Web.Controllers;
using Msr.Services.Files;
using System.Text;

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


        public FileResult Download(string Id)
        {
            var docService = new DocumentFilesService();
            var resultsFile = docService.GetSelectedRefFile(Id);

            string[] key = resultsFile.ServerPath.Split('/');
            var buketName = ConfigurationManager.AppSettings.Get("AWSBuketName");
            var streamCloud = _cloudUploader.DownloadFromCloud(buketName, key[3] + "/" + key[4]);

            var byteArray = Encoding.ASCII.GetBytes(resultsFile.ServerPath.ToString());
            var stream = new MemoryStream(byteArray);
            var extention = resultsFile.Show.Split('.');
            if (extention[1] == "pdf")
            {
                return File(streamCloud, "application/pdf", resultsFile.Show);
            }
            else if (extention[1] == "docx")
            {
                return File(streamCloud, "application/vnd.ms-word", string.Format(resultsFile.Show));
            }
            else if (extention[1] == "txt")
            {
                return File(streamCloud, "text/plain", string.Format(resultsFile.Show));
            }
            else if (extention[1] == "jpg" || extention[1] == "png" || extention[1] == "jpeg" || extention[1] == "gif")
            {
                return File(streamCloud, extention[1], string.Format(resultsFile.Show));
            }
            else if (extention[1] == "xls")
            {
                return File(streamCloud, "application/vnd.ms-excel", string.Format(resultsFile.Show));
            }
            else if (extention[1] == "xlsx")
            {
                return File(streamCloud, "application/vnd.ms-excel", string.Format(resultsFile.Show));
            }
            else if (extention[1] == "ppt")
            {
                return File(streamCloud, "application/vnd.ms-powerpoint", string.Format(resultsFile.Show));
            }
            else if (extention[1] == "zip")
            {
                return File(streamCloud, "application/zip", string.Format(resultsFile.Show));
            }
            else
            {
                return File(stream, "text/plain", string.Format(resultsFile.Show));
            }

        }
    }
}