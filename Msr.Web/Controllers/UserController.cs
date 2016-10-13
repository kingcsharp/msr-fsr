using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.Users;
using Msr.Web.ViewModel;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult Master()
        {
            return View();
        }

        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult MasterUserData(JqGridParam param)
        {

            var userService = new UserService();

            var totalRows = userService.GetUserQueryable();

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

            var clientUsers = userService.GetClientUsers(loggedUser);

            var totalRows = userService.GetUserQueryable();

            totalRows = totalRows.Where(x => clientUsers.Contains(x.Id));

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

        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult AddUser()
        {
            var userService = new UserService();

            var viewModel = new AddUserViewModel();
            viewModel.Setup(userService, new CompanyService());

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult AddUser(AddUserViewModel viewModel)
        {
            var userService = new UserService();

            if (ModelState.IsValid)
            {
                var response = userService.AddUser(viewModel.UserSummary);

                if (!response.HasErrors())
                {
                    return RedirectToAction("Master");
                }

                ModelState.AddModelError("", response.ErrorMessage());
            }

            viewModel.Setup(userService, new CompanyService());

            return View(viewModel);
        }

        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult EditUser(string id)
        {
            var userService = new UserService();
            var companyService = new CompanyService();

            var user = userService.GetUser(id);

            var viewModel = new EditUserViewModel { UserSummary = user };

            viewModel.Setup(userService, companyService);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
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

            viewModel.Setup(userService, new CompanyService());

            return View(viewModel);
        }

        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult DeleteUser(string id)
        {
            var userService = new UserService();

            var user = userService.GetUser(id);

            var viewModel = new EditUserViewModel { UserSummary = user };

            viewModel.Setup(userService, new CompanyService());

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
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

        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult AddClientUser(string id)
        {
            ViewBag.UserId = id;
            var userService = new UserService();

            var userList = userService.GetUserQueryable().Where(x => x.RoleName == RolesConstants.ClientBuyer).ToList();

            return View(userList);
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult AddClientUser(string userId, List<string> usderIds)
        {
            var userService = new UserService();

            userService.AddClientToUser(userId, usderIds, userId);

            return RedirectToAction("Client");

        }
    }
}