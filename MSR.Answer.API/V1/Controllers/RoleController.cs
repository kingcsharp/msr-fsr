using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MSR.Domain.Models;

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

        [HttpPost]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> CreateMenuRoleMap([FromBody, Required]CreateMenuRoleMapRequest request)
        {
            var command = request.ToCreateMenuRoleMapCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToCreatedResponse<string>();
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<Role>>))]
        public async Task<IActionResult> GetRoles()
        {
            var ret = await _dispatcher.DispatchAsync(new GetRoles());
            return ret.ToOkObjectResponse<ICollection<Role>>();
        }


        [HttpPatch]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> AddPermissionsToMenuRoleMap([FromBody, Required]UpdateMenuRoleMapRequest request)
        {
            var command = request.ToUpdateMenuRoleMapCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMenuRoleMap(int id)
        {
            var command = new MSR.Domain.Commands.RemoveMenuRoleMap() { Id = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

    }
}