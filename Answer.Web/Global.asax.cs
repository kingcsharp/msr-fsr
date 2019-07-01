using System;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Msr.Repositories;
using Msr.Services.jqGrid;
using Msr.Services.Roles;
using Msr.Web;

namespace Answer.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ContextDbInitializer.Seed(new ApplicationDbContext());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(JqGridParam), new JqGridParamConverter());

            IronPdf.License.LicenseKey = "IRONPDF-138372CE75-686825-423338-419A44C0B1-F5AA4668-UEx8129778D4BA18D8-CMHWORKSLLC.IRO190627.4855.33211.PRO.1DEV.1YR.SUPPORTED.UNTIL.27.JUN.2020";
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var user = HttpContext.Current.User;

                var  roleService = new RoleService();

                var userRoles = roleService.GetAssignedRolesByLogin(user.Identity.Name);

                var userPrincipal = new GenericPrincipal(HttpContext.Current.User.Identity, userRoles.Select(x => x.Role_Name).ToArray());

                Context.User = userPrincipal;
            }
        }
    }
}
