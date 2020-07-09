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

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    [AllowAnonymous]
    public class WorkflowPendingApprovalController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _dispatcher;

        public WorkflowPendingApprovalController(ILogger<WorkflowPendingApprovalController> logger, ICommandDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<PendingApprovalModel>>))]
        public async Task<IActionResult> Get([FromQuery, Required] GetPendingApprovalRequest request)
        {
            var command = request.ToGetPendingApprovalCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<ICollection<PendingApprovalModel>>();
        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> Post([FromQuery, Required] PostPendingApprovalRequest request)
        {
            var command = request.ToPostApprovalCommand();

            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<AuditActionResult>();
        }

        [HttpDelete, SwaggerResponse(typeof(AuditActionResult<PendingApprovalModel>))]
        public async Task<IActionResult> Delete([FromQuery, Required] DeletePendingApprovalRequest request)
        {
            try
            {
                var command = request.ToDeleteApprovalCommand();

                var ret = await _dispatcher.DispatchAsync(command);

                var result = ret.ToOkObjectResponse<PendingApprovalModel>("Pending Approval was cancelled successfully.");
                return result;
            }
            catch (System.Exception e)
            {

                throw;
            }
            
        }

    }
}
