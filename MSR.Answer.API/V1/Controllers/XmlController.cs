using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Extentions;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Models;
using NSwag.Annotations;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class XmlController : BaseApiController {

        private ICommandDispatcher _dispatcher;

        public XmlController(ICommandDispatcher dispatcher)
		{
            _dispatcher = dispatcher;
        }

        [HttpPatch("Transmit")]
        [SwaggerResponse(typeof(AuditActionResult<XmlTransmissionLogModel>))]
        public async Task<IActionResult> AddMessage([FromBody, Required] XmlTransmissionRequest request)
        {
            var command = request.ToTransmitXmlFileCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<WorkOrderMessageModel>("File transmitted successfully");

        }
    }
}

