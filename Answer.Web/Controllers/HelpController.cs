using System;
using System.Configuration;
using System.Web.Mvc;
using Msr.Infrastructure.Email;
using Msr.Web.ViewModel.Help;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class HelpController : Controller
    {
        [HttpPost]
        public JsonResult SupportRequest(SupportViewModel viewModel)
        {
            try
            {
            var body = $@"<table>
                    <tr><td>First Name</td><td>{viewModel.FirstName}</td></tr>
                    <tr><td>Last Name</td><td>{viewModel.LastName}</td></tr>
                    <tr><td>Email</td><td>{viewModel.Email}</td></tr>
                    <tr><td>Phone</td><td>{viewModel.Phone}</td></tr>
                    <tr><td>Subject</td><td>{viewModel.Subject}</td></tr>
                    <tr><td>ContactMathod</td><td>{viewModel.ContactMathod}</td></tr>
                    <tr><td>Details</td><td>{viewModel.Details}</td></tr>
                    </table>";

            var from = viewModel.Email;
            var to = ConfigurationManager.AppSettings["SupportEmail"];

                EmailService.SendEmail(from, to, "Support Request", body, null, true);

            return Json("OK", JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json("NOK", JsonRequestBehavior.AllowGet);
            }
        }
    }
}