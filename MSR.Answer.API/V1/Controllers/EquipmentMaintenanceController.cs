using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using MSR.Answer.API.Attributes;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
#if DEBUG
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
#endif
    public class EquipmentMaintenanceController : BaseApiController
    {
        private const string privilegeApiName = "EquipmentMaintenance";
        private readonly ICommandDispatcher _dispatcher;

        public EquipmentMaintenanceController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]//, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<EquipmentMaintenanceModel>>))]
        public async Task<IActionResult> GetEquipmentMaintenance([FromQuery] GetEquipmentMaintenanceRequest filters)
        {
            var getEquipmentMaintenance = filters.ToGetEquipmentMaintenanceCommand();
            var ret = await _dispatcher.DispatchAsync(getEquipmentMaintenance);
            return ret.ToOkObjectResponse<IEnumerable<EquipmentMaintenanceModel>>();
        }

        [HttpPost]//, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<EquipmentMaintenanceModel>))]
        public async Task<IActionResult> CreateEquipmentMaintenance([FromBody, Required] CreateEquipmentMaintenanceRequest request)
        {
            var createEquipmentMaintenance = request.ToCreateEquipmentMaintenanceCommand();
            var ret = await _dispatcher.DispatchAsync(createEquipmentMaintenance);
            return ret.ToOkObjectResponse<EquipmentMaintenanceModel>("Equipment maintenance has been successfully created.");
        }

        [HttpPatch]//, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanEdit)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<EquipmentMaintenanceModel>))]
        public async Task<IActionResult> UpdateEquipmentMaintenance([FromBody, Required] UpdateEquipmentMaintenanceRequest request)
        {
            var updateEquipmentMaintenance = request.ToUpdateEquipmentMaintenanceCommand();
            var ret = await _dispatcher.DispatchAsync(updateEquipmentMaintenance);
            return ret.ToOkObjectResponse<EquipmentMaintenanceModel>("Equipment maintenance has been successfully updated.");
        }
    }
}
