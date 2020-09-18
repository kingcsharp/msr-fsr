using System;
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
using Microsoft.AspNetCore.SignalR;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;
using MSR.Application.Hubs;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class LocationController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;
        private readonly IHubContext<MessageHub> _messageHub;

        public LocationController(ILogger<LocationController> logger, ICommandDispatcher dispatcher, IHubContext<MessageHub> messageHub)
        {
            _logger = logger;
            _dispatcher = dispatcher;
            _messageHub = messageHub;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<LocationModel>>))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanRead)]
        public async Task<IActionResult> Get([FromQuery, Required] GetLocationRequest request)
        {
            var command = request.ToGetLocationCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<LocationModel>>();
        }

        [HttpGet("{id}/Sensor"), SwaggerResponse(typeof(AuditActionResult<IEnumerable<SensorModel>>))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanRead)]
        public async Task<IActionResult> GetSensorsForLocation([FromRoute] int id)
        {
            var command = new GetSensorsForLocation() { LocationId = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<IEnumerable<SensorModel>>();

        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<LocationModel>))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> CreateLocation([FromBody, Required] CreateLocationRequest request)
        {
            var command = request.ToCreateLocationCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<LocationModel>(await DetermineResponseMessage(ret, "Create"));
        }

        [HttpPost("{locationId}/Sensor/{sensorId}"), SwaggerResponse(typeof(AuditActionResult))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> AddSensorToLocation([FromRoute, Required] int locationId, [FromRoute, Required] int sensorId)
        {
            var command = new CreateLocationSensorMap() { LocationId = locationId, SensorItemId = sensorId };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse("Sensor Successfully Added");
        }

        [HttpPatch, SwaggerResponse(typeof(AuditActionResult))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> UpdateLocation([FromBody, Required] UpdateLocationRequest request)
        {
            var command = request.ToUpdateLocationCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse(await DetermineResponseMessage(ret, "Update"));
        }


        [HttpDelete("{id}"), SwaggerResponse(typeof(AuditActionResult))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> DeactivateLocation(int id)
        {
            var command = new DeactivateLocation() { LocationId = id };
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse(await DetermineResponseMessage(ret, "Deactivate"));
        }

        [HttpDelete("{locationId}/Sensor/{sensorId}"), SwaggerResponse(typeof(AuditActionResult))]
        [HasPrivilegeApi("Locations", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> RemoveSensorFromLocation([FromRoute, Required] int locationId, [FromRoute, Required] int sensorId)
        {
            var command = new DeleteLocationSensorMap() { LocationId = locationId, SensorItemId = sensorId };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse("Sensor Successfully Removed");
        }

        private async Task<string> DetermineResponseMessage(ICommandResponse commandResponse, string action)
        {
            var location = commandResponse.ToEntity<LocationModel>();
            var response = $"Location {action} Successful";

            if (!string.IsNullOrWhiteSpace(location.Status))
            {
                await SendApprovalNotificationHubMessage(EnumApprovalTables.LocationApproval, _messageHub);
                response = $"Location {action} Pending Approval";
            }

            return response;
        }
    }
}