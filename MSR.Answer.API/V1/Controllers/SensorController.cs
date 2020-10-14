using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using NSwag.Annotations;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class SensorController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public SensorController(ILogger<SensorController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<IEnumerable<SensorModel>>))]
        public async Task<IActionResult> Get([FromQuery] GetSensorRequest request)
        {
            var command = request.ToGetSensorCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<IEnumerable<SensorModel>>();
        }

        [HttpGet("Name"), SwaggerResponse(typeof(AuditActionResult<IEnumerable<string>>))]
        public async Task<IActionResult> GetSensorName()
        {
            var command = new GetSensorName();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<IEnumerable<string>>();

        }

        [HttpGet("Value"), SwaggerResponse(typeof(AuditActionResult<SensorValueModel>))]
        public async Task<IActionResult> GetSensorValue([FromQuery] GetSensorValueRequest request)
        {
            var command = request.ToGetSensorValueCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<SensorValueModel>();

        }
    }
}
