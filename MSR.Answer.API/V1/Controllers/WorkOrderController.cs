using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class WorkOrderController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public WorkOrderController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet()]
        [HasPrivilegeApi("WipStatus", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderModel>>))]
        public async Task<IActionResult> GetWorkOrder([FromQuery] GetWorkOrderRequest request)
        {
            var command = request.ToGetWorkOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<WorkOrderModel>>();
        }

        [HttpPatch]
        [HasPrivilegeApi("WipStatus", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<WorkOrderModel>))]
        public async Task<IActionResult> UpdateWorkOrder(UpdateWorkOrderRequest request)
        {
            var command = request.ToUpdateWorkOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<WorkOrderModel>("WorkOrder updated successfully");
        }
    }
}
