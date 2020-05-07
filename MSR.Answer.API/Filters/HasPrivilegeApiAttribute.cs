using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using Newtonsoft.Json;

namespace MSR.Answer.API.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class HasPrivilegeApiAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public HasPrivilegeApiAttribute(string controllerName, EnumPrivilege enumPrivilege)
        {
            EnumMenuItem = EnumUtils.ParseMenuType(controllerName);
            EnumPrivilege = enumPrivilege;
        }
        public EnumMenuItem EnumMenuItem { get; set; }
        public EnumPrivilege EnumPrivilege { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                return;
            }

            var hasClaim = context.HttpContext.User.HasClaim(c => c.Type == "Privileges");
            if (!hasClaim)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string claimVal = context.HttpContext.User.FindFirst(c => c.Type == "Privileges").Value;

            var value = JsonConvert.DeserializeObject<int[][]>(claimVal);

            var menuItemPrivileges = value[(int)EnumMenuItem];
            if (menuItemPrivileges.IndexOf((int)EnumPrivilege) == -1)
            {
                context.Result = new UnauthorizedResult();
            }

            return;
        }
    }
}