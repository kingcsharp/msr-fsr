using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class BaseController : Controller
    {
        public string GetUserId()
        {
            return Session["UserId"].ToString();
        }  
    }
}