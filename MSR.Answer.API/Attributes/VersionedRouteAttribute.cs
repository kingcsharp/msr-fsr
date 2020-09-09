using Microsoft.AspNetCore.Mvc;

namespace MSR.Answer.API.Attributes
{
    public sealed class VersionedRouteAttribute : RouteAttribute
    {
        /// <summary>
        /// API Version
        /// </summary>
        /// <param name="route"></param>
        /// <example>1</example>
        public VersionedRouteAttribute(string route)
            : base("v{version:apiVersion}/" + route)
        {}
    }
}
