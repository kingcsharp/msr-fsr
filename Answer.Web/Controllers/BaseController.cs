using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Users;
using Msr.Services.Users.Messages;

namespace Answer.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public LoggedUserIdResult GetCurrentUser()
        {
            var userService = new UserService();

            var userId = HttpContext.User.Identity.Name;

            return userService.GetUserId(userId);
        }

        public List<string> GetDefaultStatus()
        {
            return new[] {"CREATING", "DENIED", "APPROVED", "APPROVED_BUT_REVISING", "APPROVED_BUT_DELETING"}.ToList();
        }

        public void AddErrorNotification(string message)
        {
            TempData["ErrorMessage"] = message;
        }

        public void AddSuccessNotification(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        public void AddWarningNotification(string message)
        {
            TempData["WarningMessage"] = message;
        }
    }
}