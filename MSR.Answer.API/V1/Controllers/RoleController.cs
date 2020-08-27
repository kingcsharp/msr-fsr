using System;
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
using MSR.Domain.Views;

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

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<Role>))]
        [HasPrivilegeApi("Roles", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> Post(CreateRoleRequest request)
        {
            var command = request.ToCreateRoleCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<Role>("Role successfully created");
        }

        [HttpPatch, SwaggerResponse(typeof(AuditActionResult<Role>))]
        [HasPrivilegeApi("Roles", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> Patch(UpdateRoleRequest request)
        {
            var command = request.ToUpdateRoleCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<Role>("Role successfully updated");
        }

        [HttpDelete("{id}"), SwaggerResponse(typeof(AuditActionResult))]
        [HasPrivilegeApi("Roles", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> Delete(int id)
        {
            var ret = await _dispatcher.DispatchAsync(new DeleteRole() { Id = id });
            return ret.ToOkObjectResponse("Role successfully Deleted");
        }
    }
}