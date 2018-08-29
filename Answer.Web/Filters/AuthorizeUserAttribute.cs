using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Answer.Web.Filters
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {

        public string ModuleName { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var isAuthorized = base.AuthorizeCore(httpContext);

            if (!isAuthorized)
            {
                return false;
            }

            ////TODO check module permissions from database 
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "account",
                        action = "login"
                    })
            );
        }
    }
}