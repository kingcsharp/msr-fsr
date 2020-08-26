using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dispatcher"></param>
        public AccountController(ILogger<AccountController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Main Login Process
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous, SwaggerResponse(typeof(AuditActionResult<string>))]
        public async Task<IActionResult> Login([FromBody, Required]SystemLoginRequest request)
        {
            var command = request.ToSystemLoginCommand();

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
        public async Task<IActionResult> ForgotPassword([FromBody, Required]ForgotPasswordRequest request)
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
        public async Task<IActionResult> ForgotUserName([FromBody, Required]ForgotUserNameRequest request)
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
        public async Task<IActionResult> ResetPassword([FromBody, Required]ResetPasswordRequest request)
        {
            var command = request.ToResetPasswordCommand();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToOkObjectResponse("Password reset email sent");
        }

    }
}
