using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.Users;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class DocController : BaseController
    {
        public ActionResult View(string filePath, string fileType, string fileName)
        {
            var orderService = new OrderService();
            var img = orderService.GetDocumentBase64(filePath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(Convert.FromBase64String(img), fileType);
        }
    }
}