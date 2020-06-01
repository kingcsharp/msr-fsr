using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using NSwag.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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

        [HttpPatch]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> AddPermissionsToMenuRoleMap([FromBody,Required]UpdateMenuRoleMapRequest request)
        {
            var command = request.ToUpdateMenuRoleMapCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMenuRoleMap(int id)
        {
            var command = new RemoveMenuRoleMap() { Id = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

    }
}