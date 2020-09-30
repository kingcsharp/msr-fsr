using System.Collections.Generic;
using System.Net;
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
    public class TimezoneController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;
        public TimezoneController(ILogger<SensorController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet, SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<ICollection<TimeZoneModel>>))]
        public async Task<IActionResult> GetTimezones()
        {
            var ret = await _dispatcher.DispatchAsync(new GetTimezone());
            var result = ret.ToOkObjectResponse<ICollection<TimeZoneModel>>();
            return result;
        }
    }
}