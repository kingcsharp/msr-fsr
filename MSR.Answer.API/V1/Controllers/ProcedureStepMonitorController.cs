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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    /// Procedure step monitor API
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ProcedureStepMonitorController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        /// <summary>
        /// Procedure step monitory controller Constructor
        /// </summary>
        public ProcedureStepMonitorController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Add procedure step monitor
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200"></response>
        [HttpPost]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepMonitor>))]
        public async Task<IActionResult> AddProcedureStepMonitor(CreateProcedureStepMonitorRequest body)
        {
            var command = body.ToCreateProcedureStepMonitorCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepMonitor>();
        }

        /// <summary>
        /// Delete procedure step monitor
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DeleteProcedureStepMonitor([FromRoute][Required]int id)
        {
            var command = new DeleteProcedureStepMonitor() { Id = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<bool>();
        }

        /// <summary>
        /// Get monitors for a procedure step
        /// </summary>
        /// <param name="monitorId"></param>
        /// <param name="stepId"></param>
        /// <response code="200"></response>
        [HttpGet("procedurestep/{stepId}")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureStepMonitor>>))]
        public async Task<IActionResult> GetProcedureStepMonitor([FromRoute]int stepId, int? monitorId)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureStepMonitor() {
                procedureStepId = stepId,
                procedureStepMonitorId = monitorId
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureStepMonitor>>();
        }

        /// <summary>
        /// Update procedure step monitor
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200"></response>
        [HttpPatch]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepMonitor>))]
        public async Task<IActionResult> UpdateProcedureStepMonitor(UpdateProcedureStepMonitorRequest body)
        {
            var command = body.ToUpdateProcedureStepMonitorCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepMonitor>();
        }
    }
}
