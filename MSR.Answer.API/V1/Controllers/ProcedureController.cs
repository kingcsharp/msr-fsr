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

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ProcedureController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public ProcedureController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="body"></param>
        /// <param name="version"></param>
        /// <response code="200"></response>
        [HttpPost]
        [Route("/v{version}/Procedure")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureRequest>))]
        public async Task<IActionResult> ProcedureAddProcedure([FromBody]CreateProcedureRequest newproc)
        {
            var command = newproc.ToCreateProcedureCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureRequest>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="body"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <response code="200"></response>
        [HttpPost("{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepRequest>))]
        public async Task<IActionResult> ProcedureAddProcedureStep(int id, CreateProcedureStepRequest newstep)
        {
            var command = newstep.ToCreateProcedureStepCommand();
            command.procedureId = id;
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepRequest>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <response code="200"></response>
        [HttpDelete]
        [Route("/v{version}/Procedure/{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public virtual IActionResult ProcedureDeactivateProcedure([FromRoute][Required]int? id)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(AuditActionResult));
            string exampleJson = null;
            exampleJson = "{\n  \"errorMessages\" : [ {\n    \"number\" : 0,\n    \"message\" : \"message\",\n    \"isValidationMessage\" : true\n  }, {\n    \"number\" : 0,\n    \"message\" : \"message\",\n    \"isValidationMessage\" : true\n  } ],\n  \"hasValidationErrors\" : true,\n  \"hasErrors\" : true,\n  \"id\" : 6,\n  \"successMessage\" : \"successMessage\"\n}";

                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<AuditActionResult>(exampleJson)
                        : default(AuditActionResult);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stepId"></param>
        /// <param name="version"></param>
        /// <response code="200"></response>
        [HttpDelete]
        [Route("/Robert5/msr-api/v1/v{version}/Procedure/{id}/step/{stepId}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public virtual IActionResult ProcedureDeactivateProcedureStep([FromRoute][Required]int? id, [FromRoute][Required]int? stepId, [FromRoute][Required]string version)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(AuditActionResult));
            string exampleJson = null;
            exampleJson = "{\n  \"errorMessages\" : [ {\n    \"number\" : 0,\n    \"message\" : \"message\",\n    \"isValidationMessage\" : true\n  }, {\n    \"number\" : 0,\n    \"message\" : \"message\",\n    \"isValidationMessage\" : true\n  } ],\n  \"hasValidationErrors\" : true,\n  \"hasErrors\" : true,\n  \"id\" : 6,\n  \"successMessage\" : \"successMessage\"\n}";

                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<AuditActionResult>(exampleJson)
                        : default(AuditActionResult);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="version"></param>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpGet]
        [Route("/v{version}/Procedure")]
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
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <param name="stepid"></param>
        /// <response code="404"></response>
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
        ///
        /// </summary>
        /// <param name="body"></param>
        /// <param name="version"></param>
        /// <response code="200"></response>
        [HttpPatch]
        [Route("/v{version}/Procedure")]
        [SwaggerResponse(typeof(AuditActionResultOfProcedure))]
        public async Task<IActionResult> ProcedureUpdateProcedure([FromBody]UpdateProcedureRequest newproc)
        {
            var command = newproc.ToUpdateProcedureCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureRequest>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="body"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <response code="404"></response>
        [HttpPatch("{id}/step")]
        [Route("/v{version}/Procedure/{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepRequest>))]
        public async Task<IActionResult> UpdateProcedureStep(int id, UpdateProcedureStepRequest newstep)
        {
            var command = newstep.ToUpdateProcedureStepCommand();
            command.procedureId = id;
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepRequest>();
        }
    }
}
