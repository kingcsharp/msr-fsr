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
        public async Task<IActionResult> GetWorkOrder(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetWorkOrder() {
                Id = id
            });
            return ret.ToOkObjectResponse<ICollection<WorkOrderModel>>();
        }

        [HttpPost]
        [HasPrivilegeApi("WipStatus", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<WorkOrderModel>))]
        public async Task<IActionResult> AddWorkOrder(CreateWorkOrderRequest newobj)
        {
            var command = newobj.ToCreateWorkOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<WorkOrderModel>();
        }

        [HttpPatch]
        [HasPrivilegeApi("WipStatus", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<WorkOrderModel>))]
        public async Task<IActionResult> UpdateWorkOrder(UpdateWorkOrderRequest newobj)
        {
            var command = newobj.ToUpdateWorkOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<WorkOrderModel>();
        }

        [HttpDelete]
        [HasPrivilegeApi("WipStatus", EnumPrivilege.CanDelete)]
        [SwaggerResponse(typeof(AuditActionResult<bool>))]
        public async Task<IActionResult> UpdateWorkOrder(DeleteWorkOrderRequest newobj)
        {
            var command = newobj.ToDeleteWorkOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<bool>();
        }
    }
}
