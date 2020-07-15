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
using Microsoft.AspNetCore.Authorization;
using MSR.Answer.API.V1.Models.Workflow;
using System.Collections.Generic;
using MSR.Domain.Commands;
using MSR.Answer.API.Filters;
using MSR.Domain.Commanding.Enums;

namespace MSR.Answer.API.V1.Controllers.Workflow
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    [AllowAnonymous]
    public class WorkflowGroupController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public WorkflowGroupController(ILogger<WorkflowController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<WorkflowGroupModel>>)), HasPrivilegeApi("ApprovalGroups", EnumPrivilege.CanRead)]
        public async Task<IActionResult> Get([FromQuery, Required] GetWorkflowGroupRequest request)
        {
            var command = request.ToGetWorkflowGroupCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            var result = ret.ToOkObjectResponse<ICollection<WorkflowGroupModel>>();

            return result;
        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<WorkflowGroupModel>)), HasPrivilegeApi("ApprovalGroups", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> Post([FromBody, Required] CreateWorkflowGroupRequest request)
        {
            var command = request.ToCreateWorkflowGroupCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<WorkflowGroupModel>("Workflow Group has been successfully created.");
        }

        [HttpPatch, SwaggerResponse(typeof(AuditActionResult<WorkflowGroupModel>)), HasPrivilegeApi("ApprovalGroups", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> Update([FromBody, Required] UpdateWorkflowGroupRequest request)
        {
            var command = request.ToUpdateWorkflowGroupCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<WorkflowGroupModel>("Workflow Group has been successfully updated.");
        }

        [HttpDelete("{workflowId}")]
        [HasPrivilegeApi("ApprovalGroups", EnumPrivilege.CanDelete), SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> Delete(int workflowId)
        {
            var ret = await _dispatcher.DispatchAsync(new DeactivateWorkflowGroup()
            {
                Id = workflowId
            });

            return ret.ToOkObjectResponse("Workflow Group was successfully removed.");
        }
    }
}
