using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Answer.Web.ViewModel.Images;
using Msr.Models.Orders;
using Msr.Services.Orders;
using Msr.Services.Orders.ViewModels;
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
        public JsonResult FileUploader(List<HttpPostedFileBase> files, string fillId)
        {
            var orderService = new OrderService();

            foreach (HttpPostedFileBase file in files)
            {
                var ImageModel = new SaveWorkItemImageViewModel();

                ImageModel.Name = DateTime.Now.Ticks.ToString() + file.FileName;
                ImageModel.Desc = null;

                var destinationPath = Path.Combine("C:\\my", ImageModel.Name);


                file.SaveAs(destinationPath);

                ImageModel.Path = "/Images/" + ImageModel.Name;
                ImageModel.ContentType = file.ContentType;
                ImageModel.SrcId = null;
                ImageModel.SrcName = null;
                ImageModel.SrcDesc = null;
                ImageModel.SrcPath = null;
                ImageModel.SrcContentType = null;
                ImageModel.SrcChanged = null;
                ImageModel.DocChanged = null;
                ImageModel.DropSrc = "YES";
                ImageModel.NTLogin = "1618";
                ImageModel.FillID = fillId;

                orderService.SaveOrderItemImages(ImageModel);

            }

            var OrderItemImages = orderService.GetOrderItemImagesById(Id: fillId);

            foreach (var item in OrderItemImages)
            {
                var destinationPath = Path.Combine("C:\\my", item.FILE_NAME);
                item.Size = new FileInfo(destinationPath).Length;
            }

            UploadedImageView Images = new UploadedImageView();
            var imagesList = Images.MapToDto(OrderItemImages);

            return Json(new { files = imagesList.ToArray() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetImages(int id)
        {
            var viewModel = new ImageViewModel();
            viewModel.Id = id;

            return PartialView("_Images", viewModel);
        }

        public JsonResult GetImagesById(string id)
        {
            var orderService = new OrderService();

            var images = orderService.GetOrderItemImagesById(id);

            foreach (var item in images)
            {
                var destinationPath = Path.Combine("C:\\my\\", item.FILE_NAME);

                var filInfo= new FileInfo(destinationPath);

                item.Size = filInfo.Length;
            }

            var imageView = new UploadedImageView();

            var imagesList = imageView.MapToDto(images);

            return Json(new { files = imagesList.ToArray() }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeleteImageById(string Id, string FillId)
        {
            var orderService = new OrderService();

            if (!string.IsNullOrEmpty(Id))
            {
                var response = orderService.DeleteOrderItemImageById(Id: Id);

                if (response)
                {
                    var OrderItemImages = orderService.GetOrderItemImagesById(Id: FillId);
                    return Json(new { Message = "Image deleted successfully.", files = OrderItemImages.ToArray() }, JsonRequestBehavior.AllowGet);
                }
                
                return Json(new { Message = "Image upload failed." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Message = "Something went wrong." }, JsonRequestBehavior.AllowGet);
        }

    }
}