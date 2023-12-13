using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;
        private readonly IAccountService _accountService; // Import the AccountService interface or class
        private readonly IUserService _userService; // Import the UserService interface or class
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(ILogger<AccountController> logger, ICommandDispatcher dispatcher, IAccountService accountService, IUserService userService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _dispatcher = dispatcher;
            _accountService = accountService; // Initialize _accountService
            _userService = userService; // Initialize _userService
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Main Login Process
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous, SwaggerResponse(typeof(AuditActionResult<string>))]
        public async Task<IActionResult> Login([FromBody, Required] SystemLoginRequest request)
        {
            // Deactivate user using DeactivateUserAsync method
            var deactivateCommand = new DeactivateUser() { AccountId = request.AccountId };
            //await _accountService.DeactivateUserAsync(deactivateCommand);
            await _userService.DeactivateUserAsync(deactivateCommand);

            var user = await _unitOfWork.Users.FirstOrDefaultAsync(false, i => i.Id == request.AccountId);

            _logger.LogInformation($"Before deactivation check. User ID: {request.AccountId}");
            if (user != null && !user.IsActive)
            {
                _logger.LogInformation($"User is deactivated. User ID: {request.AccountId}");
                // Return a response indicating that the login process should not continue
                return StatusCode(403, new { Message = "User account is deactivated. Login is not allowed." });
            }
            _logger.LogInformation($"After deactivation check. User ID: {request.AccountId}");


            // Continue with your login logic

            var command = request.ToSystemLoginCommand();

            Microsoft.Extensions.Primitives.StringValues value = "";
            HttpContext.Request.Headers.TryGetValue("Referer", out value);

            command.Host = value;

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToOkObjectResponse<string>();
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("forgotpassword")]
        [AllowAnonymous, SwaggerResponse(System.Net.HttpStatusCode.NoContent, typeof(void))]
        public async Task<IActionResult> ForgotPassword([FromBody, Required] ForgotPasswordRequest request)
        {
            var command = request.ToForgotPasswordCommand();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToOkObjectResponse("Forgot Password email sent");
        }

        /// <summary>
        /// Forgot Username
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("forgotusername")]
        [AllowAnonymous, SwaggerResponse(System.Net.HttpStatusCode.NoContent, typeof(void))]
        public async Task<IActionResult> ForgotUserName([FromBody, Required] ForgotUserNameRequest request)
        {
            var command = request.ToForgotUserNameCommand();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToOkObjectResponse("Forgot UserName email sent");
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("resetpassword")]
        [AllowAnonymous, SwaggerResponse(System.Net.HttpStatusCode.NoContent, typeof(void))]
        public async Task<IActionResult> ResetPassword([FromBody, Required] ResetPasswordRequest request)
        {
            var command = request.ToResetPasswordCommand();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToOkObjectResponse("Password was updated!");
        }

        [HttpPatch("resetmypassword")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, typeof(void))]
        public async Task<IActionResult> ResetMyPassword([FromBody, Required] ResetMyPasswordRequest request)
        {
            //doing file change to cause reload.
            var command = request.ToResetMyPasswordCommand();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToOkObjectResponse("Password was successfully updated.");
        }
    }
}
