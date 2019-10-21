using System.Web.Http;
using Microsoft.AspNet.OData.Extensions;
using Newtonsoft.Json.Serialization;

namespace Answer.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.AddODataQueryFilter();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            //config.MapHttpAttributeRoutes();
            config.Select().Expand().Filter().OrderBy().MaxTop(200).Count();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}