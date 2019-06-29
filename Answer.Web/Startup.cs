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
            IronPdf.License.LicenseKey = "IRONPDF-138372CE75-686825-423338-419A44C0B1-F5AA4668-UEx8129778D4BA18D8-CMHWORKSLLC.IRO190627.4855.33211.PRO.1DEV.1YR.SUPPORTED.UNTIL.27.JUN.2020";
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
