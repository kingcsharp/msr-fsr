using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Models;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public UsersController(ILogger<UsersController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]GetUsersRequest request)
        {
            var user = User.Identity.Name;

            if (user is null) return BadRequest();

            var command = request.ToGetUsersCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<ICollection<User>>();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody, Required] CreateUserRequest request)
        {
            var user = User.Identity.Name;

            if (user is null) return BadRequest();

            var command = request.ToCreateUserCommand(user);

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToCreatedResponse<User>();
        }
    }
}