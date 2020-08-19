using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.Filters;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    public class ProcedureTypeApiController : ControllerBase
    {
        private ICommandDispatcher _dispatcher;

        public ProcedureTypeController(ICommandDispatcher dispatcher)
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
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureTypeRequest>))]
        public async Task<IActionResult> AddProcedureType(CreateProcedureTypeRequest newproc)
        {
            var command = newproc.ToCreateProcedureTypeCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureTypeRequest>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <response code="200"></response>
        [HttpDelete]
        [Route("/Robert5/msr-api/v1/v{version}/ProcedureType/{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public virtual IActionResult ProcedureTypeDeactivateProcedureType([FromRoute][Required]int? id, [FromRoute][Required]string version)
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
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureTypeRequest>>))]
        public async Task<IActionResult> GetProcedureType(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureType() {
                Id = id
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureTypeRequest>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="body"></param>
        /// <param name="version"></param>
        /// <response code="200"></response>
        [HttpPatch]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureTypeRequest>))]
        public async Task<IActionResult> UpdateProcedureType(UpdateProcedureTypeRequest newproc)
        {
            var command = newproc.ToUpdateProcedureTypeCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureTypeRequest>();
        }
    }
}
