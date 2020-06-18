using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<bool>))]
        public async Task<IActionResult> GetPartsAsync()
        {
            var ret = await _dispatcher.DispatchAsync(new GetParts());
            return ret.ToOkObjectResponse<ICollection<bool>>();
        }

        [HttpGet("{id}")]
        [SwaggerResponse(typeof(AuditActionResult<bool>))]
        public IActionResult GetPart(int id)
        {
            return new OkObjectResult(new AuditActionResult<bool>()
            {
                Object = true,
                SuccessMessage = "gets a part",
            });;
        }
    }
}
