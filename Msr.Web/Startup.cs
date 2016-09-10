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
        }
    }
}
