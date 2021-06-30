using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MSR.Domain.Abstractions.Services;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    /// Procedure Controller
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ProcedureController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        /// <summary>
        /// Procedure Controller
        /// </summary>
        public ProcedureController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Add Procedure
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200"></response>
        [HttpPost]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<Procedure>))]
        public async Task<IActionResult> ProcedureAddProcedure([FromBody] CreateProcedureRequest body)
        {
            var command = body.ToCreateProcedureCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            string message = "Procedure successfully added";
            if (ret.DisplayString != null)
            {
                message = ret.DisplayString;
            }

            return ret.ToOkObjectResponse<Procedure>(message);
        }

        /// <summary>
        /// Copy Procedure
        /// </summary>
        /// <param name="procedureId"></param>
        /// <returns></returns>
        [HttpPost("copy/{procedureId}")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<Procedure>))]
        public async Task<IActionResult> ProcedureCopyProcedure([FromRoute][Required] int procedureId)
        {
            var command = new CopyProcedure() {
                SourceProcedureId = procedureId
            };
            var ret = await _dispatcher.DispatchAsync(command);

            return ret.ToOkObjectResponse<Procedure>("Procedure successfully copied: " + ret.DisplayString);
        }

        /// <summary>
        /// Add Procedure Step
        /// </summary>
        /// <param name="body"></param>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpPost("{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepModel>))]
        public async Task<IActionResult> ProcedureAddProcedureStep(int id, CreateProcedureStepRequest body)
        {
            var command = body.ToCreateProcedureStepCommand();
            command.procedureId = id;
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepModel>(
                DetermineResponseMessage<ProcedureStepModel>(ret, "add", "Procedure Step")
            );
        }

        /// <summary>
        /// Delete Procedure
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> ProcedureDeleteProcedure([FromRoute][Required] int id)
        {
            var command = new DeleteProcedure() { procedureID = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse("Procedure successfully deleted");
        }

        /// <summary>
        /// Delete Procedure Step
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stepid"></param>
        /// <response code="200"></response>
        [HttpDelete("{id}/step/{stepid}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> ProcedureDeleteProcedureStep([FromRoute][Required] int id, [FromRoute][Required] int stepid)
        {
            var command = new DeleteProcedureStep() { procedureID = id, procedureStepID = stepid };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse(
                DetermineResponseMessage<ProcedureStepModel>(ret, "delete", "Procedure Step")
            );
        }

        /// <summary>
        /// Get one of all Procedures
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<Procedure>>))]
        public async Task<IActionResult> ProcedureGetProcedure([FromQuery] GetProcedureRequest request)
        {
            var command = request.ToGetProcedureCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<Procedure>>();
        }

        /// <summary>
        /// Get Procedure Step
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stepid"></param>
        /// <response code="200"></response>
        [HttpGet("{id}/step")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureStepModel>>))]
        public async Task<IActionResult> GetProcedureStep(int id, int? stepid)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureStep()
            {
                procedureId = id,
                stepId = stepid
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureStepModel>>();
        }

        /// <summary>
        /// Update Procedure
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200"></response>
        [HttpPatch]
        [SwaggerResponse(typeof(AuditActionResult<Procedure>))]
        public async Task<IActionResult> ProcedureUpdateProcedure([FromBody] UpdateProcedureRequest body)
        {
            var command = body.ToUpdateProcedureCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<Procedure>("Procedure successfully updated");
        }

        /// <summary>
        /// Update Procedure Step
        /// </summary>
        /// <param name="body"></param>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpPatch("{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepModel>))]
        public async Task<IActionResult> UpdateProcedureStep(int id, UpdateProcedureStepRequest body)
        {
            var command = body.ToUpdateProcedureStepCommand();
            command.procedureId = id;
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepModel>(
                DetermineResponseMessage<ProcedureStepModel>(ret, "update", "Procedure Step")
            );
        }
    }
}
