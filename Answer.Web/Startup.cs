using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Msr.Services.Jobs;
using Owin;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(Msr.Web.Startup))]
namespace Msr.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            IronPdf.License.LicenseKey = ConfigurationManager.AppSettings["IronPdf.LicenseKey"];
            ////var options = new DashboardOptions
            ////{
            ////    AuthorizationFilters = new[]
            ////    {
            ////        new LocalRequestsOnlyAuthorizationFilter()
            ////    }
            ////};

            ////GlobalConfiguration.Configuration.UseSqlServerStorage("MsrPortal");
            ////app.UseHangfireDashboard("/hangfire",options);
            ////app.UseHangfireServer();

            //// RecurringJob.AddOrUpdate("A_SP_ADMIN_SQL_TO_RUN_EXECUTE", () => SpAdminJob.Run(), "*/1 * * * *");
        }
    }
}
