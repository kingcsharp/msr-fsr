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
    public class ProcedureTypeController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public ProcedureTypeController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet()]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureType>>))]
        public async Task<IActionResult> GetProcedureType(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureType() {
                Id = id
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureType>>();
        }

        [HttpPost]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureType>))]
        public async Task<IActionResult> AddProcedureType(CreateProcedureTypeRequest newproc)
        {
            var command = newproc.ToCreateProcedureTypeCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureType>();
        }

        [HttpPatch]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureType>))]
        public async Task<IActionResult> UpdateProcedureType(UpdateProcedureTypeRequest newproc)
        {
            var command = newproc.ToUpdateProcedureTypeCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureType>();
        }
    }
}
