using Microsoft.AspNetCore.Mvc;

namespace MSR.Answer.API.Attributes
{
    public sealed class VersionedRouteAttribute : RouteAttribute
    {
        public VersionedRouteAttribute(string route)
            : base("v{version:apiVersion}/" + route)
        {}
    }
}
