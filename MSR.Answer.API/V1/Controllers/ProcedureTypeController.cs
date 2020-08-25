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
    /// Procedure Type API
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ProcedureTypeController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        /// <summary>
        /// Procedure type controller constructor
        /// </summary>
        public ProcedureTypeController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Add a procedure type
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200"></response>
        [HttpPost]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureTypeRequest>))]
        public async Task<IActionResult> AddProcedureType(CreateProcedureTypeRequest body)
        {
            var command = body.ToCreateProcedureTypeCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureTypeRequest>();
        }

        /// <summary>
        /// Delete a procedure type
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(AuditActionResult))]
        public async Task<IActionResult> DeleteProcedureType([FromRoute][Required]int id)
        {
            var command = new DeleteProcedureType() { id = id };
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<bool>();
        }

        /// <summary>
        /// Get one or all procedure types
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"></response>
        [HttpGet]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureType>>))]
        public async Task<IActionResult> GetProcedureType(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureType() {
                Id = id
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureType>>();
        }

        /// <summary>
        /// Update a procedure type
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200"></response>
        [HttpPatch]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureType>))]
        public async Task<IActionResult> UpdateProcedureType(UpdateProcedureTypeRequest body)
        {
            var command = body.ToUpdateProcedureTypeCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureType>();
        }
    }
}
