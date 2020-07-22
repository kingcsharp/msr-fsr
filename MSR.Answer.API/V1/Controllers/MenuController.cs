using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class MenuController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public MenuController(ILogger<AccountController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<IEnumerable<MenuItem>>))]
        public async Task<IActionResult> GetMenu()
        {
            if (UserId == 0) return BadRequest();

            var command = new GetMenu();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToOkObjectResponse<IEnumerable<MenuItem>>();
        }



        [HttpPost("Role")]
        [SwaggerResponse(typeof(AuditActionResult<string>))]
        public async Task<IActionResult> CreateMenuRoleMap([FromBody, Required] CreateMenuRoleMapRequest request)
        {
            var command = request.ToCreateMenuRoleMapCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<string>();
        }

        [HttpPatch("Role")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> AddPermissionsToMenuRoleMap([FromBody, Required] UpdateMenuRoleMapRequest request)
        {
            var command = request.ToUpdateMenuRoleMapCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse();
        }

        [HttpDelete("Role/{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> RemoveMenuRoleMap(int id)
        {
            var command = new RemoveMenuRoleMap() { Id = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse();
        }
    }
}
