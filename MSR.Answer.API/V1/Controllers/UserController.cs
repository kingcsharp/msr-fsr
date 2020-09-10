
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Application.Hubs;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using NSwag.Annotations;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class UserController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;
        private readonly IHubContext<MessageHub> _messageHub;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dispatcher"></param>
        public UserController(ILogger<UserController> logger, ICommandDispatcher dispatcher, IHubContext<MessageHub> messageHub)
        {
            _logger = logger;
            _dispatcher = dispatcher;
            _messageHub = messageHub;
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<UserModel>>)), HasPrivilegeApi("Users", EnumPrivilege.CanRead)]
        public async Task<IActionResult> GetUsers([FromQuery, Required] GetUsersRequest request)
        {
            var command = request.ToGetUsersCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<ICollection<UserModel>>();
        }

        /// <summary>
        /// Get logged in user data
        /// </summary>
        /// <returns></returns>
        [HttpGet("LoggedInUser"), SwaggerResponse(typeof(AuditActionResult<UserModel>))]
        public async Task<IActionResult> GetLoggedInUserData()
        {
            var command = new GetLoggedInUserData() { UserId = UserId };

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<UserModel>();
        }

        /// <summary>
        /// Get training certificates
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGetAttribute("TrainingCertification"), SwaggerResponse(typeof(AuditActionResult<IEnumerable<TrainingCertificationView>>))]
        public async Task<IActionResult> GetTrainingCertificationData([FromQuery] GetTrainingCertificationRequest request)
        {
            var command = new GetTrainingCertification { Id = request.UserId };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<IEnumerable<TrainingCertificationView>>();

        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, HasPrivilegeApi("Users", EnumPrivilege.CanCreate), SwaggerResponse(typeof(AuditActionResult<UserModel>))]
        public async Task<IActionResult> CreateUser([FromBody, Required] CreateUserRequest request)
        {
            var command = request.ToCreateUserCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            //TODO HANDLE WORKFLOW TO SEND THIS NOTIFICATION
            //await _messageHub.Clients.All.SendAsync("WorkflowNotification",  new Guid(), new PendingNotificationItem()
            //{
            //    Table = (int)EnumApprovalTables.UserApproval,
            //    Count = 1
            //});

            return ret.ToOkObjectResponse<UserModel>("User has been successfully created.");
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch, HasPrivilegeApi("Users", EnumPrivilege.CanEdit), SwaggerResponse(typeof(AuditActionResult<UserModel>))]
        public async Task<IActionResult> UpdateUser([FromBody, Required] UpdateUserRequest request)
        {
            var command = request.ToUpdateUserCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            //TODO HANDLE WORKFLOW TO SEND THIS NOTIFICATION
            //await _messageHub.Clients.All.SendAsync("WorkflowNotification",  new Guid(), new PendingNotificationItem()
            //{
            //    Table = (int)EnumApprovalTables.UserApproval,
            //    Count = 1
            //});

            return ret.ToOkObjectResponse<UserModel>("User has been successfully updated.");
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// <example>2147483647</example>
        [HttpDelete("{accountId}"), HasPrivilegeApi("Users", EnumPrivilege.CanDelete), SwaggerResponse(typeof(void))]
        public async Task<IActionResult> DeactivateUser(int accountId)
        {
            var command = new DeactivateUser()
            {
                AccountId = accountId
            };

            var ret = await _dispatcher.DispatchAsync(command);

            //TODO HANDLE WORKFLOW TO SEND THIS NOTIFICATION
            //await _messageHub.Clients.All.SendAsync("WorkflowNotification",  new Guid(), new PendingNotificationItem()
            //{
            //    Table = (int)EnumApprovalTables.UserApproval,
            //    Count = 1
            //});

            return ret.ToOkObjectResponse("User has been Deactivated");
        }

        /// <summary>
        /// Assign role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/Role"), HasPrivilegeApi("Users", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> AssignRoleToUser([FromBody, Required] CreateUserRoleRequest request)
        {
            var command = request.ToCreateUserRoleCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<UserModel>("Role assigned to user successfully");
        }

        /// <summary>
        /// Edit user role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("/Role"), HasPrivilegeApi("Users", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> EditUserRole([FromBody, Required] UpdateUserRoleRequest request)
        {
            var command = request.ToUpdateUserRoleCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse("User Roles Updated");
        }

        /// <summary>
        /// Remove user role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/Role/{id}"), HasPrivilegeApi("Users", EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> RemoveUserRole(int id)
        {
            var command = new DeleteUserRole() { UserRoleId = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse("Role removed from User");
        }
    }
}
