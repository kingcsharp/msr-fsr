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
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;

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

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<LocationModel>>))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanRead)]
        public async Task<IActionResult> Get([FromQuery, Required]GetLocationRequest request)
        {
            var command = request.ToGetLocationCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<LocationModel>>();
        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<LocationModel>))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> CreateLocation([FromBody, Required] CreateLocationRequest request)
        {
            var command = request.ToCreateLocationCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<LocationModel>($"Location Created Successfully");
        }

        [HttpPatch, SwaggerResponse(typeof(AuditActionResult))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> UpdateLocation([FromBody, Required] UpdateLocationRequest request)
        {
            var command = request.ToUpdateLocationCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse("Location Updated Successfully");
        }

        [HttpDelete("{id}"), SwaggerResponse(typeof(AuditActionResult))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> DeactivateLocation(int id)
        {
            var command = new DeactivateLocation() { LocationId = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse("Location Deactivated Successfully");
        }
    }
}