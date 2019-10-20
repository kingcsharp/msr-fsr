using System.Web.Http;
using System.Web.Http.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Msr.Models.Reporting;

namespace Answer.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.AddODataQueryFilter();

            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<CombinedFinancialData>("CombinedFinancialData");

            config.MapODataServiceRoute(
                routeName: "CombinedFinancialData",
                routePrefix: null,
                model: (Microsoft.OData.Edm.IEdmModel)modelBuilder.GetEdmModel()
            );

            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}