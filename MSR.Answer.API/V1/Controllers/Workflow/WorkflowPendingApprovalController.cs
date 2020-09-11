using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Models;
using System.Threading.Tasks;
using MSR.Answer.API.V1.Models;
using NSwag.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using MSR.Answer.API.Filters;
using MSR.Application.Hubs;
using MSR.Domain.Commanding.Enums;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class WorkflowPendingApprovalController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;
        private readonly IHubContext<MessageHub> _messageHub;
        //PendingApprovals
        public WorkflowPendingApprovalController(ILogger<WorkflowPendingApprovalController> logger, ICommandDispatcher dispatcher, IHubContext<MessageHub> messageHub)
        {
            _logger = logger;
            _dispatcher = dispatcher;
            _messageHub = messageHub;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<PendingApprovalModel>>)), HasPrivilegeApi("PendingApprovals", EnumPrivilege.CanRead)]
        public async Task<IActionResult> Get([FromQuery, Required] GetPendingApprovalRequest request)
        {
            var command = request.ToGetPendingApprovalCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<ICollection<PendingApprovalModel>>();
        }
        //, HasPrivilegeApi("PendingApprovals", EnumPrivilege.CanRead)
        [HttpGet("Details"), SwaggerResponse(typeof(AuditActionResult<PendingApprovalPopoverModel>))]
        public async Task<IActionResult> GetApprovalDetails([FromQuery, Required] GetPendingApprovalDetailRequest request)
        {
            var command = request.ToGetPendingApprovalDetailsCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<PendingApprovalPopoverModel>();
        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<PendingApprovalModel>)), HasPrivilegeApi("PendingApprovals", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> Post([FromBody, Required] PostPendingApprovalRequest request)
        {
            var command = request.ToPostApprovalCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            await SendApprovalNotificationHubMessage(request.Table, _messageHub, -1);

            return ret.ToOkObjectResponse<PendingApprovalModel>("Pending Approval was approved successfully.");
        }

        [HttpDelete, SwaggerResponse(typeof(AuditActionResult<PendingApprovalModel>)), HasPrivilegeApi("PendingApprovals", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> Delete([FromQuery, Required] DeletePendingApprovalRequest request)
        {
            var command = request.ToDeleteApprovalCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            await SendApprovalNotificationHubMessage(request.Table, _messageHub, -1);

            var result = ret.ToOkObjectResponse<PendingApprovalModel>("Pending Approval was cancelled successfully.");
            return result;
        }

    }
}
