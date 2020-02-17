using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Msr.Web.Startup))]
namespace Msr.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            IronPdf.License.LicenseKey = "IRONPDF-138372CE75-686825-423338-419A44C0B1-F5AA4668-UEx8129778D4BA18D8-CMHWORKSLLC.IRO190627.4855.33211.PRO.1DEV.1YR.SUPPORTED.UNTIL.27.JUN.2020";
        }
    }
}
