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
using System.Net;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    /// ProcedureStepTemplateController
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ProcedureStepTemplateController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dispatcher"></param>
        public ProcedureStepTemplateController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Get procedure step template by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureStepTemplateModel>>))]
        public async Task<IActionResult> GetProcedureStepTemplate(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureStepTemplate() {
                Id = id
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureStepTemplateModel>>();
        }

        /// <summary>
        /// Add procedure step template
        /// </summary>
        /// <param name="newproc"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanCreate)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepTemplateModel>))]
        public async Task<IActionResult> AddProcedureStepTemplate(CreateProcedureStepTemplateRequest newproc)
        {
            var command = newproc.ToCreateProcedureStepTemplateCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepTemplateModel>("New procedure template successfully submitted");
        }

        /// <summary>
        /// Update procedure step template
        /// </summary>
        /// <param name="newproc"></param>
        /// <returns></returns>
        [HttpPatch]
        [HasPrivilegeApi("RunnableProcedures", EnumPrivilege.CanEdit)]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepTemplateModel>))]
        public async Task<IActionResult> UpdateProcedureStepTemplate(UpdateProcedureStepTemplateRequest newproc)
        {
            var command = newproc.ToUpdateProcedureStepTemplateCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepTemplateModel>("Procedure template successfully updated");
        }
    }
}
