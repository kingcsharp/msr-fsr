using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Controllers;
using Msr.Infrastructure.Email;
using Msr.Models.Helps;
using Msr.Models.Parts;
using Msr.Services.Helps;
using Msr.Services.Helps.ViewModels;
using Msr.Services.jqGrid;
using Msr.Web.ViewModel.Engineering;
using Msr.Web.ViewModel.Help;
using Msr.Services.Roles;
using Answer.Web.Filters;
using Answer.Web.ViewModel.Help;
using Answer.Web.UserMailer;

namespace Msr.Web.Controllers
{
    [AuthorizeUser]
    public class HelpController : BaseController
    {
        private readonly HelpService _helpService;
        private readonly IUserMailer _userMailer;

        public HelpController()
        {
            _userMailer = new UserMailer();
            _helpService = new HelpService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Helps";

            return View(viewModel);
        }

        public ActionResult HelpsData(JqGridParam param)
        {

            var totalRows = _helpService.GetHelpQueryable();


            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(HelpView.Title))
                    {
                        totalRows = totalRows.Where(x => x.Title == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(HelpView.Content))
                    {
                        totalRows = totalRows.Where(x => x.Content.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(HelpView.FriendlyUrl))
                    {
                        totalRows = totalRows.Where(x => x.FriendlyUrl.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(HelpView.RoleName))
                    {
                        totalRows = totalRows.Where(x => x.RoleName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(HelpView.Id))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Id == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.Id.ToString().ToLower() == rule.data.ToLower());
                        }
                    }

                }
            }
            var orderBy = nameof(PartsView.CompanyPartNumber);

            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                orderBy = param.sortColumn;
            }

            if (param.sortOrder == "desc")
            {
                totalRows = totalRows.OrderByDescending(orderBy);
            }
            else
            {
                totalRows = totalRows.OrderBy(orderBy);
            }

            var totalRecords = totalRows.Count();
            totalRows = totalRows.Skip(param.pageSize * (param.pageIndex - 1));
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);

            var results = totalRows.ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SupportTicket()
        {
            var suppoertModel = new SupportViewModel();

            return PartialView("_SupportTicket", suppoertModel);
        }

        [HttpPost]
        public JsonResult SupportRequest(SupportViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json("Invalid data", JsonRequestBehavior.AllowGet);
                }

                var mailMessage = _userMailer.SendSupportRequest(new SupportRequestModel()
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    Subject = viewModel.Subject,
                    ContactMethod = viewModel.ContactMethod,
                    Details = viewModel.Details
                });
                EmailService.SendEmail(viewModel.Email, ConfigurationManager.AppSettings["SupportEmail"],mailMessage.Subject, mailMessage.Body, null, true);
                //var body = $@"<!DOCTYPE html><html><body><table>
                //    <tr><td>First Name</td><td>{viewModel.FirstName}</td></tr>
                //    <tr><td>Last Name</td><td>{viewModel.LastName}</td></tr>
                //    <tr><td>Email</td><td>{viewModel.Email}</td></tr>
                //    <tr><td>Phone</td><td>{viewModel.Phone}</td></tr>
                //    <tr><td>Subject</td><td>{viewModel.Subject}</td></tr>
                //    <tr><td>Contact Method</td><td>{viewModel.ContactMethod}</td></tr>
                //    <tr><td>Details</td><td>{viewModel.Details.Replace(Environment.NewLine, "<br/>")}</td></tr>
                //    </table></body></html>";

                //var from = viewModel.Email;
                //var to = ConfigurationManager.AppSettings["SupportEmail"];

                //EmailService.SendEmail(from, to, "Support Request", body, null, true);

                return Json("OK", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json("Something went wrong", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create()
        {
            var helpViewmodel = new HelpViewModel();

            helpViewmodel.SetUp(new RoleService());

            return View(helpViewmodel);
        }
        [HttpPost]
        public ActionResult Create(HelpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _helpService.Create(model: model);
                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Help Created Successfully";
                    return RedirectToAction("index");
                }
                TempData["ErrorMessage"] = response.ErrorMessage;
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {

            var model = _helpService.GetById(id);

            var help = new HelpViewModel();

            help = help.MapToDto(model);

            help.SetUp(new RoleService());

            return View(help);
        }

        [HttpPost]
        public ActionResult Edit(HelpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _helpService.Edit(model: model);
                if (!response.HasErrors())
                {
                    TempData["SuccessMessage"] = "Help Updated Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something Went Wrong";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var model = _helpService.Delete(id);

            if (!model.HasErrors())
            {
                TempData["SuccessMessage"] = "Help deleted successfully.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }

        public ActionResult GetHelpDetails(string pageUrl)
        {
            var helpPage = _helpService.GetHelp(pageUrl);

            if (helpPage == null)
            {
                helpPage = new HelpPage() { Title = $"Page not found, Url :'{pageUrl}'" };
            }

            return PartialView("Help/_HelpView", helpPage);
        }

        [HttpPost]
        public JsonResult CheckFrindlyUrl(string friendlyUrl)
        {
            var result = _helpService.CheckUrl(friendlyUrl);

            if (result != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
    }
}