using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public MenuController(ILogger<AccountController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            var user = User.Identity.Name;

            if (user is null) return BadRequest();

            int.TryParse(user, out var userId);

            var command = new GetMenu() { UserId = userId };

            await _dispatcher.DispatchAsync(command);

            return Ok();
        }
    }
}