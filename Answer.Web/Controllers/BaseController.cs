using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class BaseController : Controller
    {

        public string GetUserId()
        {
            ////todo  exec A_SP_PEOPLE_GET_DATA_BY_ID  1618,1618
            Session["UserId"] = 1618;
            return Session["UserId"].ToString();
        }  
    }
}