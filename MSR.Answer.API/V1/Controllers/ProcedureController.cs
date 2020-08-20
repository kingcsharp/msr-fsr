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
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpPost]
        [Route("/Procedure")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureRequest>))]
        public async Task<IActionResult> ProcedureAddProcedure([FromBody]CreateProcedureRequest body)
        {
            var command = body.ToCreateProcedureCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureRequest>();
        }

        /// <summary>
        /// Add Procedure Step
        /// </summary>
        /// <param name="body"></param>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpPost("{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepRequest>))]
        public async Task<IActionResult> ProcedureAddProcedureStep(int id, CreateProcedureStepRequest body)
        {
            var command = body.ToCreateProcedureStepCommand();
            command.procedureId = id;
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepRequest>();
        }

        /// <summary>
        /// Deactivate Procedure
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpDelete]
        [Route("/Procedure/{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> ProcedureDeactivateProcedure([FromRoute][Required]int? id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deactivate Procedure Step
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stepId"></param>
        /// <response code="200"></response>
        [HttpDelete]
        [Route("/Procedure/{id}/step/{stepId}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> ProcedureDeactivateProcedureStep([FromRoute][Required]int? id, [FromRoute][Required]int? stepId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get one of all Procedures
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/Procedure")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureRequest>>))]
        public async Task<IActionResult> ProcedureGetProcedure(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedure() {
                procedureID = id
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureRequest>>();
        }

        /// <summary>
        /// Get Procedure Step
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stepid"></param>
        /// <response code="200"></response>
        [HttpGet("{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureStepRequest>>))]
        public async Task<IActionResult> GetProcedureStep(int id, int? stepid)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureStep() {
                procedureId = id,
                stepId = stepid
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureStepRequest>>();
        }

        /// <summary>
        /// Update Procedure
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200"></response>
        [HttpPatch]
        [Route("/Procedure")]
        [SwaggerResponse(typeof(AuditActionResultOfProcedure))]
        public async Task<IActionResult> ProcedureUpdateProcedure([FromBody]UpdateProcedureRequest body)
        {
            var command = body.ToUpdateProcedureCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureRequest>();
        }

        /// <summary>
        /// Update Procedure Step
        /// </summary>
        /// <param name="body"></param>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpPatch]
        [Route("/Procedure/{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepRequest>))]
        public async Task<IActionResult> UpdateProcedureStep(int id, UpdateProcedureStepRequest body)
        {
            var command = body.ToUpdateProcedureStepCommand();
            command.procedureId = id;
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepRequest>();
        }
    }
}
