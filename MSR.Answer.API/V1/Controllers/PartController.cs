using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using NSwag.Annotations;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class PartController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public PartController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<bool>))]
        public IActionResult GetParts()
        {
            return new OkObjectResult(new AuditActionResult<bool>()
            {
                Object = true,
                SuccessMessage = "Hey you guys",
            });;
        }
    }
}
