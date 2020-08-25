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
    public class ProcedureStepTemplateController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public ProcedureStepTemplateController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet()]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureStepTemplateModel>>))]
        public async Task<IActionResult> GetProcedureStepTemplate(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureStepTemplate() {
                Id = id
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureStepTemplateModel>>();
        }

        [HttpPost]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepTemplateModel>))]
        public async Task<IActionResult> AddProcedureStepTemplate(CreateProcedureStepTemplateRequest newproc)
        {
            var command = newproc.ToCreateProcedureStepTemplateCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepTemplateModel>();
        }

        [HttpPatch]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepTemplateModel>))]
        public async Task<IActionResult> UpdateProcedureStepTemplate(UpdateProcedureStepTemplateRequest newproc)
        {
            var command = newproc.ToUpdateProcedureStepTemplateCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepTemplateModel>();
        }
    }
}
