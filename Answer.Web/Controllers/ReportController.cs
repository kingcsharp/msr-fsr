using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.Users;
using Answer.Web.Controllers;
using Answer.Web.Filters;

namespace Msr.Web.Controllers
{
    [AuthorizeUser]
    public class ReportController : BaseController
    {
        //[HttpGet,Route("reports/{reportID}")]
        public ActionResult Index()
        {
            ViewBag.ActiveClass = "WIP";

            var loggedUser = User.Identity.GetUserId();

            var userService = new UserService();

            //var company = userService.GetCompanyId(loggedUser);
            
            //ViewBag.ClientName = company.Name;

            return View();
        }

    }
}