using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Msr.Models.Reporting;

namespace Answer.Web
{
    public class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.AddODataQueryFilter();
            config.EnableDependencyInjection();

            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<CombinedFinancialData>("CombinedFinancialData");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "api/Reporting",
                model: modelBuilder.GetEdmModel()
            );
        }
    }
}