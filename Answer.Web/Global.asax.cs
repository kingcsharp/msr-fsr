using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Msr.Repositories;
using Msr.Services.jqGrid;
using Msr.Web;

namespace Answer.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ContextDbInitializer.Seed(new ApplicationDbContext());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(JqGridParam), new JqGridParamConverter());
        }
    }
}
