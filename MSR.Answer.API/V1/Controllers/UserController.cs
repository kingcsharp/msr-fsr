
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
    public class UserController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public UserController(ILogger<UserController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<User>>)), HasPrivilegeApi("Users", EnumPrivilege.CanRead)]
        public async Task<IActionResult> GetUsers([FromQuery, Required] GetUsersRequest request)
        {
            var command = request.ToGetUsersCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<ICollection<User>>();
        }

        [HttpGet("LoggedInUser"), SwaggerResponse(typeof(AuditActionResult<User>))]
        public async Task<IActionResult> GetLoggedInUserData()
        {
            var command = new GetLoggedInUserData() { UserId = UserId };

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<User>();
        }

        [HttpPost, HasPrivilegeApi("Users", EnumPrivilege.CanCreate), SwaggerResponse(typeof(AuditActionResult<User>))]
        public async Task<IActionResult> CreateUser([FromBody, Required] CreateUserRequest request)
        {
            var command = request.ToCreateUserCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToCreatedResponse<User>();
        }

        [HttpPatch, HasPrivilegeApi("Users", EnumPrivilege.CanEdit), SwaggerResponse(typeof(AuditActionResult<User>))]
        public async Task<IActionResult> UpdateUser([FromBody, Required] UpdateUserRequest request)
        {
            var command = request.ToUpdateUserCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToNoContentResponse();
        }

        [HttpDelete("{accountId}"), HasPrivilegeApi("Users", EnumPrivilege.CanDelete), SwaggerResponse(typeof(void))]
        public async Task<IActionResult> DeactivateUser(int accountId)
        {
            var command = new DeactivateUser()
            {
                AccountId = accountId
            };

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToNoContentResponse();
        }

        [HttpPost("/Role"), HasPrivilegeApi("Users", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> AssignRoleToUser([FromBody, Required] CreateUserRoleRequest request)
        {
            var command = request.ToCreateUserRoleCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToCreatedResponse<Location>();
        }

        [HttpPatch("/Role"), HasPrivilegeApi("Users", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> EditUserRole([FromBody, Required] UpdateUserRoleRequest request)
        {
            var command = request.ToUpdateUserRoleCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }

        [HttpDelete("/Role/{id}"), HasPrivilegeApi("Users",EnumPrivilege.CanDelete)]
        public async Task<IActionResult> RemoveUserRole(int id)
        {
            var command = new DeleteUserRole() { UserRoleId = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToNoContentResponse();
        }
    }
}