using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Answer.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Msr.Infrastructure.Email;
using Msr.Infrastructure.Helpers;
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
        private readonly PeopleService _peopleService;
        public AccountController()
        {
            _userService = new UserService();
            _peopleService = new PeopleService();
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.User = model.User.Trim().ToLower();

            var answerUser = _peopleService.GetAnswerUser(model.User, model.Password);

            if (answerUser.HasErrors())
            {
                ModelState.AddModelError("", answerUser.ErrorMessage);
                return View(model);
            }

            model.Password = "msr" + model.User + "$";

            var result = await SignInManager.PasswordSignInAsync(model.User, model.Password, model.RememberMe, false);

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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAnserByUserName(model.UserName);

                if (user == null || user.PortalUser)
                {
                    ModelState.AddModelError("", $"User not found with Id: '{model.UserName}'");
                    return View(model);
                }

                var from = ConfigurationManager.AppSettings["From"];
                var websiteUrl = ConfigurationManager.AppSettings["WebsiteUrl"];

                var encryptedText = EncryptionHelper.Encrypt(user.Login).Replace('/', '*');

                var lnkHref = $"<a href='{websiteUrl}/Account/ResetPassword?token={encryptedText}'>Reset Password</a>";

                var body = $@"<div>
               <p>Hello ANSWER user,<br/></p>
               <p>This email is being sent to you due to a password reset request from the MSR-FSR Answer system.<br/></p>
               <p><b> Please reset your password by clicking : </ b ><br/> </p>
               <p>{lnkHref}</p>
                        </div>";

                var subject = "ANSWER - Reset password";

                try
                {
                    EmailService.SendEmail(from, user.Email, subject, body, null, true);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "There is an error when sending email");

                    return View(model);
                }

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {
            token = Request.Url.Query.Replace("?token=", "");

            var decUser = EncryptionHelper.Decrypt(token.Replace('*', '/'));

            var user = _userService.GetAnserByUserName(decUser);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid token, Please reset password again";

                return RedirectToAction("ResetPassword");
            }

            var viewModel = new ResetPasswordViewModel {UserId = user.Id };

            return View(viewModel);
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

        [AllowAnonymous]
        public ActionResult ForgotUserNameConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotUserName()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotUserName(ForgotUserNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SendUsername(model.Email);

                if (result.HasErrors())
                {
                    ModelState.AddModelError("", result.ErrorMessage);

                    return View(model);
                }

                return RedirectToAction("ForgotUserNameConfirmation", "Account");
            }

            return View(model);
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