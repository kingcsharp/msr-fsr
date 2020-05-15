
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public UserController(ILogger<UserController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [SwaggerResponse(typeof(AuditActionResult<ICollection<User>>)), HasPrivilegeApi("Users", EnumPrivilege.CanRead)]
        public async Task<IActionResult> GetUsers([FromQuery, Required]GetUsersRequest request)
        {
            if (UserId == 0)
            {
                return BadRequest();
            }

            var command = request.ToGetUsersCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<ICollection<User>>();
        }

        [HttpGet("LoggedInUser"), SwaggerResponse(typeof(AuditActionResult<User>))]
        public async Task<IActionResult> GetLoggedInUserData()
        {
            if (UserId == 0) return BadRequest();

            var command = new GetLoggedInUserData() { UserId = UserId };

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<User>();
        }

        [HttpPost, HasPrivilegeApi("Users", EnumPrivilege.CanCreate), SwaggerResponse(typeof(AuditActionResult<User>))]
        public async Task<IActionResult> CreateUser([FromBody, Required] CreateUserRequest request)
        {
            if (UserId == 0)
            {
                return BadRequest();
            }

            var command = request.ToCreateUserCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToCreatedResponse<User>();
        }

        [HttpPatch, HasPrivilegeApi("Users", EnumPrivilege.CanEdit), SwaggerResponse(typeof(AuditActionResult<User>))]
        public async Task<IActionResult> UpdateUser([FromBody, Required]UpdateUserRequest request)
        {
            if (UserId == 0)
            {
                return BadRequest();
            }

            var command = request.ToUpdateUserCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<User>();
        }

        [HttpDelete("{accountId}"), HasPrivilegeApi("Users", EnumPrivilege.CanDelete), SwaggerResponse(typeof(void))]
        public async Task<IActionResult> DeactivateUser(int accountId)
        {
            if (UserId == 0)
            {
                return BadRequest();
            }

            var command = new DeactivateUser()
            {
                AccountId = accountId
            };

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToNoContentResponse();
        }
    }
}