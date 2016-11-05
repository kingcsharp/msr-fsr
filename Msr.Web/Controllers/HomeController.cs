using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Infrastructure.Email;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string emailtest()
        {
            var from = ConfigurationManager.AppSettings["From"];

            var restlt = EmailService.SendEmail(from, "test", "Portal Login", "test", null, true);

            return restlt.ToString();
        }
    }
}