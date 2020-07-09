using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
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
        [SwaggerResponse(typeof(AuditActionResult<ICollection<Part>>))]
        public async Task<IActionResult> GetPartsAsync()
        {
            var ret = await _dispatcher.DispatchAsync(new GetParts());
            return ret.ToOkObjectResponse<ICollection<Part>>();
        }

        [HttpGet("{id}")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<Part>>))]
        public async Task<IActionResult> GetPart(int id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetParts() {
                partID = id
            });
            return ret.ToOkObjectResponse<ICollection<Part>>();
        }

        [HttpPost]
        [SwaggerResponse(typeof(AuditActionResult<Part>))]
        public async Task<IActionResult> AddPart(CreatePartRequest newpart)
        {
            var command = newpart.ToCreatePartCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<Part>();
        }

        [HttpPatch]
        [SwaggerResponse(typeof(AuditActionResult<Part>))]
        public async Task<IActionResult> UpdatePart(UpdatePartRequest newpart)
        {
            var command = newpart.ToUpdatePartCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<Part>();
        }
    }
}
