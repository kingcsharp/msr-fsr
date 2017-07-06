using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.Orders.ViewModels;
using Msr.Services.Users;

namespace Msr.Web.Controllers
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

        public JsonResult FileUploader(List<HttpPostedFileBase> files, string fillId)
        {
            var orderService = new OrderService();

            foreach (HttpPostedFileBase file in files)
            {
                var ImageModel = new SaveWorkItemImageViewModel();

                ImageModel.Name = file.FileName;
                ImageModel.Desc = null;

                var destinationPath = Path.Combine(Server.MapPath("~/Images/"), ImageModel.Name);


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

                TempData["SuccessMessage"] = "Images Uploaded successfully.";
            }

            TempData["ErrorMessage"] = "Something went wrong.";

            return Json(new { Success = "False" }, JsonRequestBehavior.AllowGet);
        }
    }
}