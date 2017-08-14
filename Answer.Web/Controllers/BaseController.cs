using System.Web.Mvc;
using Msr.Services.Users;
using Msr.Services.Users.Messages;

namespace Answer.Web.Controllers
{
    public class BaseController : Controller
    {
        public LoggedUserIdResult GetCurrentUser()
        {
            var userService = new UserService();

            var userId = HttpContext.User.Identity.Name;

            return userService.GetUserId(userId);
        }
    }
}