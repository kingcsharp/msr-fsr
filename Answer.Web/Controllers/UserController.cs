using System.Web.Mvc;
using Answer.Web.ViewModel;
using Msr.Services.Users;

namespace Answer.Web.Controllers
{
    public class UserController : BaseController
    {
        public UserController()
        {
            ViewBag.ActiveClass = "USER";
        }

        public ActionResult Profile()
        {
            var currentUser = GetCurrentUser();

            ViewBag.ActiveClass = "PROF";

            var viewModel = new UserProfileViewModel();

            var userService = new UserService();

            viewModel.UserSummary = userService.GetAnserByUserName(currentUser.Login);
            viewModel.Id = viewModel.UserSummary.Id;

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

            var result = userService.ValidatePassword(viewModel.Id, viewModel.Password);

            if (!result)
            {
                TempData["ErrorMessage"] = "Current password is not valid";

                viewModel.UserSummary = userService.GetAnserByUserName(currentUser.Login);

                return View(viewModel);
            }

            userService.UpdatePassword(viewModel.Id, viewModel.NewPassword);

            TempData["SuccessMessage"] = "Password has been updated successfully.";

            return RedirectToAction("Profile");
        }
    }
}