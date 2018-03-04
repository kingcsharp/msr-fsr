using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Answer.Web.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Msr.Infrastructure.Email;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.TimeZones;
using Msr.Services.Users;
using Msr.Services.Users.ViewModels;
using Msr.Web.ViewModel;
using Msr.Services.Companies;
using Answer.Web.Controllers;
using Msr.Web.Models;

namespace Msr.Web.Controllers
{
    public class UserController : BaseController
    {
        public UserController()
        {
            ViewBag.ActiveClass = "USER";
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Profile()
        {
            var currentUser = GetCurrentUser();

            ViewBag.ActiveClass = "PROF";

            var viewModel = new UserProfileViewModel();

            var userService = new UserService();

            viewModel.UserSummary = userService.GetAnserByUserName(currentUser.Login);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Profile(UserProfileViewModel viewModel)
        {
            var currentUser = GetCurrentUser();
            var userService = new UserService();

            if (!ModelState.IsValid)
            {
                viewModel.UserSummary = userService.GetAnserByUserName(currentUser.Login);

                return View(viewModel);
            }

            userService.UpdatePassword(currentUser.Id, viewModel.NedwPassword);

            TempData["SuccessMessage"] = "Profile has been updated successfully.";

            return RedirectToAction("Profile");
        }
    }
}