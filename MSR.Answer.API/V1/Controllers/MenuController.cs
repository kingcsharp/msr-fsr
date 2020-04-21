using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Domain.Commanding.Abstractions;
using System.Threading.Tasks;
using MSR.Answer.API.Attributes;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    [ApiController]
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
            return Ok();
        }
    }
}