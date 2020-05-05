using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Models;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public AccountController(ILogger<AccountController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody, Required]SystemLoginRequest request)
        {
            var command = request.ToSystemLoginCommand();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToOkObjectResponse<string>();
        }
                                                                                                                                                                                                                                                                                                                                                                                                                        
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody, Required]ForgotPasswordRequest request)
        {
            var command = request.ToForgotPasswordCommand();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToNoContentResponse();
        }

        [HttpPost("forgotusername")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotUserName([FromBody, Required]ForgotUserNameRequest request)
        {
            var command = request.ToForgotUserNameCommand();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToNoContentResponse();
        }

        [HttpPatch("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody, Required]ResetPasswordRequest request)
        {
            var command = request.ToResetPasswordCommand();

            var result = await _dispatcher.DispatchAsync(command);

            return result.ToNoContentResponse();
        }

    }
}