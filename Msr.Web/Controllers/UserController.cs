using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Msr.Infrastructure.Email;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.TimeZones;
using Msr.Services.Users;
using Msr.Web.ViewModel;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        public UserController()
        {
            ViewBag.ActiveClass = "USER";
        }

        [Authorize(Roles = nameof(RolesConstants.AnswerUser))]
        public ActionResult Master()
        {
            return View();
        }

        [Authorize(Roles = nameof(RolesConstants.AnswerUser))]
        public ActionResult MasterUserData(JqGridParam param)
        {

            var userService = new UserService();

            var totalRows = userService.GetUserQueryable().Where(x=> x.RoleName != RolesConstants.AnswerUser);

            string orderBy = nameof(PeopleView.FirstName);
            string orderDirection = "asc";

            param.sortColumn = param.sortColumn.Replace(" asc, ", "");

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
            totalRows = totalRows.Skip(param.pageIndex - 1);
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);

            var results = totalRows.Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.FullName,
                x.RoleName,
                x.Phone,
                x.Phone2,
                x.UserName,
                x.PasswordHash,
                x.Email,
                x.IsActive,
                x.TimeZone,
                x.CreatedDate,
                x.CompanyName,
                x.Status
            }).ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = nameof(RolesConstants.ClientAdmin))]
        public ActionResult Client()
        {
            return View();
        }

        [Authorize(Roles = nameof(RolesConstants.ClientAdmin))]
        public ActionResult ClientUserData(JqGridParam param)
        {
            var loggedUser = User.Identity.GetUserId();

            var userService = new UserService();

           var user =  userService.GetUser(loggedUser);

            var totalRows = userService.GetUserQueryable();

            totalRows = totalRows.Where(x => x.ParentId !=null && x.CompanyId == user.CompanyId);

            string orderBy = nameof(UserView.FirstName);
            string orderDirection = "asc";

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
            totalRows = totalRows.Skip(param.pageIndex - 1);
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);


            var results = totalRows.Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.FullName,
                x.RoleName,
                x.Phone,
                x.Phone2,
                x.UserName,
                x.PasswordHash,
                x.Email,
                x.IsActive,
                x.TimeZone,
                x.CreatedDate
            }).ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = nameof(RolesConstants.AnswerUser))]
        public ActionResult AddUser()
        {
            var userService = new UserService();

            var viewModel = new AddUserViewModel();
            viewModel.Setup(userService, new CompanyService());

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolesConstants.AnswerUser))]
        public ActionResult AddUser(AddUserViewModel viewModel)
        {
            var userService = new UserService();

            if (ModelState.IsValid)
            {
                var loggedUser = User.Identity.GetUserId();

                var response = userService.AddUser(viewModel.UserSummary, loggedUser);

                if (!response.HasErrors())
                {
                    var loginUrl = Url.Action("Login", "Account");
                    var from = ConfigurationManager.AppSettings["From"];
                    var body = "Login Portal by clicking <a href=\"" + loginUrl + "\">here</a>";

                    EmailService.SendEmail(from, viewModel.UserSummary.Email, "Portal Login", body, null, true);

                    return RedirectToAction("Master");
                }

                ModelState.AddModelError("", response.ErrorMessage());
            }

            viewModel.Setup(userService, new CompanyService());

            return View(viewModel);
        }

        [Authorize(Roles = nameof(RolesConstants.AnswerUser))]
        public ActionResult EditUser(string id)
        {
            var userService = new UserService();
            var companyService = new CompanyService();
            var timeZoneService = new TimeZoneService();

            var user = userService.GetUser(id);

            var viewModel = new EditUserViewModel { UserSummary = user };

            viewModel.Setup(userService, companyService, timeZoneService);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolesConstants.AnswerUser))]
        public ActionResult EditUser(EditUserViewModel viewModel)
        {
            var userService = new UserService();

            if (ModelState.IsValid)
            {
                var response = userService.UpdateUser(viewModel.UserSummary);

                if (!response.HasErrors())
                {
                    return RedirectToAction("Master");
                }

                ModelState.AddModelError("", response.ErrorMessage());
            }

            viewModel.Setup(userService, new CompanyService(), new TimeZoneService());

            return View(viewModel);
        }

        [Authorize(Roles = nameof(RolesConstants.AnswerUser))]
        public ActionResult DeleteUser(string id)
        {
            var userService = new UserService();

            var user = userService.GetUser(id);

            var viewModel = new EditUserViewModel { UserSummary = user };

            viewModel.Setup(userService, new CompanyService(), new TimeZoneService());

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolesConstants.AnswerUser))]
        public ActionResult DeleteUser(EditUserViewModel viewModel)
        {
            var userService = new UserService();

            var response = userService.DeleteUser(viewModel.UserSummary.Id);

            if (!response.HasErrors())
            {
                return RedirectToAction("Master");
            }

            return RedirectToAction("DeleteUser", new { viewModel.UserSummary.Id });
        }

        [Authorize(Roles = nameof(RolesConstants.ClientAdmin))]
        public ActionResult AddClientUser(string id)
        {
            var userService = new UserService();

            var viewModel = new AddClientUserViewModel();
            viewModel.Setup(userService, new CompanyService(), new TimeZoneService());

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolesConstants.ClientAdmin))]
        public ActionResult AddClientUser(AddClientUserViewModel viewModel)
        {
            var userService = new UserService();

            if (ModelState.IsValid)
            {
                var loggedUser = User.Identity.GetUserId();

                var clientAdmin = userService.GetUser(loggedUser);

                viewModel.UserSummary.CompanyId = clientAdmin.CompanyId;

                var response = userService.AddUser(viewModel.UserSummary, loggedUser);

                if (!response.HasErrors())
                {
                    var loginUrl = Url.Action("Login", "Account");
                    var from = ConfigurationManager.AppSettings["From"];
                    var websiteUrl = ConfigurationManager.AppSettings["WebsiteUrl"];

                    var body = $"Login Portal by clicking <a href=\"{websiteUrl + loginUrl}\">here</a>";

                    EmailService.SendEmail(from, viewModel.UserSummary.Email, "Portal Login", body, new List<string> {clientAdmin.Email}, true);


                    return RedirectToAction("Client");
                }

                ModelState.AddModelError("", response.ErrorMessage());
            }

            viewModel.Setup(userService, new CompanyService(), new TimeZoneService());

            return View(viewModel);
        }


        [Authorize(Roles = nameof(RolesConstants.ClientAdmin))]
        public ActionResult EditClientUser(string id)
        {
            var userService = new UserService();
            var companyService = new CompanyService();

            var user = userService.GetUser(id);

            var viewModel = new EditClientUserViewModel { UserSummary = user };

            viewModel.Setup(userService, companyService);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolesConstants.ClientAdmin))]
        public ActionResult EditClientUser(EditClientUserViewModel viewModel)
        {
            var userService = new UserService();

            if (ModelState.IsValid)
            {
                var response = userService.UpdateClientUser(viewModel.UserSummary);

                if (!response.HasErrors())
                {
                    return RedirectToAction("Client");
                }

                ModelState.AddModelError("", response.ErrorMessage());
            }

            viewModel.Setup(userService, new CompanyService());

            return View(viewModel);
        }

        public ActionResult Profile()
        {
            ViewBag.ActiveClass = "PROF";

            var viewModel = new UserProfileViewModel();

            var userService = new UserService();

            var loggedUserId = User.Identity.GetUserId();

            viewModel.UserSummary = userService.GetUserQueryable().SingleOrDefault(x => x.Id == loggedUserId);

            return View(viewModel);
        }


        public ActionResult EditProfile()
        {
            ViewBag.ActiveClass = "PROF";

            var viewModel = new EditUserProfileViewModel();

            var userService = new UserService();

            var loggedUserId = User.Identity.GetUserId();

            var user = userService.GetUserQueryable().SingleOrDefault(x => x.Id == loggedUserId);

            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            viewModel.Phone = user.Phone;
            viewModel.Phone2 = user.Phone2;
            viewModel.Email = user.Email;

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult EditProfile(EditUserProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var userService = new UserService();

            var loggedUserId = User.Identity.GetUserId();

            var userSummary = new UserSummary();

            userSummary.Id = loggedUserId;
            userSummary.FirstName = viewModel.FirstName;
            userSummary.LastName = viewModel.LastName;
            userSummary.Phone = viewModel.Phone;
            userSummary.Phone2 = viewModel.Phone2;
            userSummary.Email = viewModel.Email;

            userService.UpdateUserProfile(userSummary);

            return RedirectToAction("Profile");
        }
    }
}