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
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepMonitorRequest>))]
        public async Task<IActionResult> AddProcedureStepMonitor(CreateProcedureStepMonitorRequest body)
        {
            var command = body.ToCreateProcedureStepMonitorCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepMonitorRequest>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> ProcedureStepMonitorDeactivateProcedureStepMonitor([FromRoute][Required]int? id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpGet]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureStepMonitorRequest>>))]
        public async Task<IActionResult> GetProcedureStepMonitor(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureStepMonitor() {
                procedureStepMonitorId = id
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureStepMonitorRequest>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200"></response>
        [HttpPatch]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepMonitorRequest>))]
        public async Task<IActionResult> UpdateProcedureStepMonitor(UpdateProcedureStepMonitorRequest body)
        {
            var command = body.ToUpdateProcedureStepMonitorCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepMonitorRequest>();
        }
    }
}
