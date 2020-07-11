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
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ProcedureController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public ProcedureController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet()]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<Procedure>>))]
        public async Task<IActionResult> GetProcedure(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedure() {
                procedureID = id
            });
            return ret.ToOkObjectResponse<ICollection<Procedure>>();
        }

        [HttpPost]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<Procedure>))]
        public async Task<IActionResult> AddProcedure(CreateProcedureRequest newproc)
        {
            var command = newproc.ToCreateProcedureCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<Procedure>();
        }

        [HttpPatch]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<Procedure>))]
        public async Task<IActionResult> UpdateProcedure(UpdateProcedureRequest newproc)
        {
            var command = newproc.ToUpdateProcedureCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<Procedure>();
        }

        [HttpGet("{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureStep>>))]
        public async Task<IActionResult> GetProcedureStep(int id, int? stepid)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureStep() {
                procedureId = id,
                stepId = stepid
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureStep>>();
        }

        [HttpPost("{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStep>))]
        public async Task<IActionResult> AddProcedureStep(int id, CreateProcedureStepRequest newstep)
        {
            var command = newstep.ToCreateProcedureStepCommand();
            command.procedureId = id;
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStep>();
        }

        [HttpPatch("{id}/step")]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStep>))]
        public async Task<IActionResult> UpdateProcedureStep(int id, UpdateProcedureStepRequest newstep)
        {
            var command = newstep.ToUpdateProcedureStepCommand();
            command.procedureId = id;
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStep>();
        }
    }
}
