using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSR.Domain.Models;
using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Extentions;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class RoleController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public RoleController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        
        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<Role>>))]
        [HasPrivilegeApi("RoleModulePermission", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> GetRoles()
        {
            var ret = await _dispatcher.DispatchAsync(new GetRoles());
            return ret.ToOkObjectResponse<ICollection<Role>>();
        }
    }
}