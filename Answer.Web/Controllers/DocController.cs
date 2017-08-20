using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Msr.Models.Orders;
using Msr.Services.Orders;
using Msr.Services.Orders.ViewModels;
using Msr.Services.S3;
using Msr.Web.Controllers;

namespace Answer.Web.Controllers
{
    [Authorize]
    public class DocController : BaseController
    {
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

            var response = orderService.DeleteOrderItemImageById(id, "1618");

                if (response)
                {
                    var orderItemImages = orderService.GetOrderItemImagesById(taskId);

                    return Json(new {Message = "Image deleted successfully.", files = orderItemImages.ToArray()}, JsonRequestBehavior.AllowGet);
                }

            return Json(new {Message = "Image upload failed."}, JsonRequestBehavior.AllowGet);
        }

    }
}