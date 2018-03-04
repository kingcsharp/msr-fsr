using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Msr.Infrastructure.Email;
using Msr.Infrastructure.Helpers;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Repositories;
using Msr.Services.Orders;
using Msr.Services.Users;
using Msr.Web.Models;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly UserService _userService;
        public AccountController()
        {
            _userService = new UserService();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            model.User = model.User.Trim().ToLower();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userService = new UserService();
            var peopleService = new PeopleService();
            UserSummary user;

            var answerUser = peopleService.GetAnswerUser(model.User, model.Password);

            if (answerUser != null)
            {
                model.Password = "msr" + answerUser.Id + "$";
            }

            user = userService.GetByUserName(model.User);

            if (user != null && !user.IsActive)
            {
                TempData["WarningMessage"] = "Account has been disabled";
                return RedirectToAction("Login", "Account");
            }

            var result = await SignInManager.PasswordSignInAsync(model.User, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:

                    if (!string.IsNullOrWhiteSpace(returnUrl) && returnUrl != "/")
                    {
                        return RedirectToLocal(returnUrl);
                    }

                    return RedirectToAction("StatusView", "Wip");

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAnserByUserName(model.UserName);

                if (user == null)
                {
                    ModelState.AddModelError("", $"User not found with Id: '{model.UserName}'");
                    return View(model);
                }

                var from = ConfigurationManager.AppSettings["From"];
                var websiteUrl = ConfigurationManager.AppSettings["WebsiteUrl"];

                var lnkHref = $"<a href='{websiteUrl}/Account/ResetPassword?userId={user.Login}'>Reset Password</a>";

                string body = "<b>Please reset your password by clicking  </b><br/>" + lnkHref;

                string subject = "Reset password";

                EmailService.SendEmail(from, user.Email, subject, body, null, true);

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code, string userId)
        {
            var viewModel = new ResetPasswordViewModel();
            viewModel.UserId = userId;

            return code == null ? View("Error") : View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetAnserByUserName(model.UserId);

            if (user == null)
            {
                ModelState.AddModelError("", $"User not found with Id: '{model.UserId}'");

                return RedirectToAction("ResetPassword");
            }

            _userService.UpdatePassword(model.UserId, model.Password);

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}