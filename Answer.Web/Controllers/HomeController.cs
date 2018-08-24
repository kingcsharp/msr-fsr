using System.Configuration;
using System.Web.Mvc;
using Answer.Web.Controllers;
using Answer.Web.Filters;
using Msr.Infrastructure.Email;

namespace Msr.Web.Controllers
{
    [AuthorizeUser]
    public class HomeController : BaseController
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

        public string emailtest(string id)
        {
            var from = ConfigurationManager.AppSettings["From"];

            var restlt = EmailService.SendEmail(from, id, "Portal Login", "test", null, true);

            return restlt.ToString();
        }
    }
}