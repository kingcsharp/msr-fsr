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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using MSR.Answer.API.V1.Models.Workflow;
using System.Collections.Generic;
using MSR.Domain.Commands.Workflow;

namespace MSR.Answer.API.V1.Controllers
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

        [HttpGet("pending"), SwaggerResponse(typeof(AuditActionResult<PendingApprovalNotification>))]
        public async Task<IActionResult> GetPendingApprovals()
        {
            var ret = await _dispatcher.DispatchAsync(new GetPendingApprovals());

            return ret.ToOkObjectResponse<PendingApprovalNotification>();
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<WorkflowGroupModel>>))]
        public async Task<IActionResult> Get([FromQuery, Required] GetWorkflowGroupRequest request)
        {
            var command = request.ToGetWorkflowGroupCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            var result = ret.ToOkObjectResponse<ICollection<WorkflowGroupModel>>();

            return result;
        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<WorkflowGroupModel>))]
        public async Task<IActionResult> Post([FromBody, Required] CreateWorkflowGroupRequest request)
        {
            var command = request.ToCreateWorkflowGroupCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<WorkflowGroupModel>("Workflow Group has been successfully created.");
        }

        [HttpPatch, SwaggerResponse(typeof(AuditActionResult<WorkflowGroupModel>))]
        public async Task<IActionResult> Update([FromBody, Required] UpdateWorkflowGroupRequest request)
        {
            var command = request.ToUpdateWorkflowGroupCommand();
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<WorkflowGroupModel>("Workflow Group has been successfully updated.");
        }

        [HttpDelete("{workflowId}"), SwaggerResponse(typeof(void))]
        public async Task<IActionResult> Delete(int workflowId)
        {
            var ret = await _dispatcher.DispatchAsync(new DeactivateWorkflow()
            {
                Id = workflowId
            });

            return ret.ToNoContentResponse();
        }
    }
}