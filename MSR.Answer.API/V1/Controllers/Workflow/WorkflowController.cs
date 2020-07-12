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
using Microsoft.AspNetCore.Authorization;
using MSR.Answer.API.V1.Models.Workflow;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MSR.Answer.API.Filters;
using MSR.Domain.Commanding.Enums;

namespace MSR.Answer.API.V1.Controllers.Workflow
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    [AllowAnonymous]
    public class WorkflowController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public WorkflowController(ILogger<WorkflowController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet("pending"), SwaggerResponse(typeof(AuditActionResult<PendingApprovalNotification>)), HasPrivilegeApi("ApprovalWorkflows", EnumPrivilege.CanRead)]
        public async Task<IActionResult> GetPendingApprovals()
        {
            var ret = await _dispatcher.DispatchAsync(new GetPendingApprovalsModel());

            return ret.ToOkObjectResponse<PendingApprovalNotification>();
        }

        [HttpGet("activity"), SwaggerResponse(typeof(AuditActionResult<ICollection<WorkflowActivityModel>>)), HasPrivilegeApi("ApprovalWorkflows", EnumPrivilege.CanRead)]
        public async Task<IActionResult> GetWorkflowActivities()
        {
            var ret = await _dispatcher.DispatchAsync(new GetWorkflowActivities());

            return ret.ToOkObjectResponse<ICollection<WorkflowActivityModel>>();
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<WorkflowModel>>)), HasPrivilegeApi("ApprovalWorkflows", EnumPrivilege.CanRead)]
        public async Task<IActionResult> Get([FromQuery, Required] GetWorkflowRequest request)
        {
            var command = request.ToGetWorkflowCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            var result = ret.ToOkObjectResponse<ICollection<WorkflowModel>>();

            return result;
        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<WorkflowModel>)), HasPrivilegeApi("ApprovalWorkflows", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> Post([FromBody, Required] CreateWorkflowRequest request)
        {
            var command = request.ToCreateWorkflowGroupCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<WorkflowModel>("Workflow has been successfully created.");
        }

        [HttpPatch, SwaggerResponse(typeof(AuditActionResult<WorkflowModel>)), HasPrivilegeApi("ApprovalWorkflows", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> Update([FromBody, Required] UpdateWorkflowRequest request)
        {
            var command = request.ToUpdateWorkflowGroupCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<WorkflowModel>("Workflow has been successfully updated.");
        }

        [HttpDelete("{workflowId}"), SwaggerResponse(typeof(void)), HasPrivilegeApi("ApprovalWorkflows", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> Delete(int workflowId)
        {
            var ret = await _dispatcher.DispatchAsync(new DeactivateWorkflowModel()
            {
                Id = workflowId
            });

            return ret.ToNoContentResponse();
        }


    }
}