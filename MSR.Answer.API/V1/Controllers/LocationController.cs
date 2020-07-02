using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Threading.Tasks;
using MSR.Answer.API.V1.Models;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class LocationController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public LocationController(ILogger<LocationController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet(), SwaggerResponse(typeof(AuditActionResult<Location>))]
        public async Task<IActionResult> Get([FromQuery, Required]GetLocations request)
        {
            var ret = await _dispatcher.DispatchAsync(request);

            return ret.ToOkObjectResponse<ICollection<Location>>();
        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<Location>))]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationRequest request) 
        {
            return Ok();
        }
    }
}