using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Domain.Commanding.Abstractions;
using System.Threading.Tasks;
using MSR.Answer.API.V1.Models;
using NSwag.Annotations;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using MSR.Domain.Models;
using MSR.Domain.Commands;
using MSR.Answer.API.Filters;
using MSR.Domain.Commanding.Enums;

namespace MSR.Answer.API.V1.Controllers.Workflow
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    [AllowAnonymous]
    public class WorkflowStageController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public WorkflowStageController(ILogger<WorkflowController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<WorkflowStageModel>>)), HasPrivilegeApi("ApprovalStages", EnumPrivilege.CanRead)]
        public async Task<IActionResult> Get([FromQuery, Required] GetWorkflowStageRequest request)
        {
            var command = request.ToGetWorkflowStageCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            var result = ret.ToOkObjectResponse<ICollection<WorkflowStageModel>>();

            return result;
        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<WorkflowStageModel>)), HasPrivilegeApi("ApprovalStages", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> Post([FromBody, Required] CreateWorkflowStageRequest request)
        {
            var command = request.ToCreateWorkflowStageCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<WorkflowStageModel>("Workflow Stage has been successfully created.");
        }

        [HttpPatch, SwaggerResponse(typeof(AuditActionResult<WorkflowStageModel>)), HasPrivilegeApi("ApprovalStages", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> Update([FromBody, Required] UpdateWorkflowStageRequest request)
        {
            var command = request.ToUpdateWorkflowStageCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<WorkflowStageModel>("Workflow Stage has been successfully updated.");
        }

        [HttpDelete("{workflowId}")]
        [ProducesResponseType(typeof(void), 204), HasPrivilegeApi("ApprovalStages", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> Delete(int workflowId)
        {
            var ret = await _dispatcher.DispatchAsync(new DeactivateWorkflowGroup()
            {
                Id = workflowId
            });

            return ret.ToNoContentResponse();
        }
    }
}
