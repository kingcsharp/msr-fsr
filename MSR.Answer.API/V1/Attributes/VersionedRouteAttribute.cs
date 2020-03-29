using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Attributes
{
    public sealed class VersionedRouteAttribute : RouteAttribute
    {
        public VersionedRouteAttribute(string route)
            : base("v{version:apiVersion}/" + route)
        {}
    }
}
