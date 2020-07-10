using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ProcedureStepMonitorController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public ProcedureStepMonitorController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet()]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<ProcedureStepMonitor>>))]
        public async Task<IActionResult> GetProcedureStepMonitor(int? id)
        {
            var ret = await _dispatcher.DispatchAsync(new GetProcedureStepMonitor() {
                procedureID = id
            });
            return ret.ToOkObjectResponse<ICollection<ProcedureStepMonitor>>();
        }

        [HttpPost]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepMonitor>))]
        public async Task<IActionResult> AddProcedureStepMonitor(CreateProcedureStepMonitorRequest newproc)
        {
            var command = newproc.ToCreateProcedureStepMonitorCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepMonitor>();
        }

        [HttpPatch]
        [SwaggerResponse(typeof(AuditActionResult<ProcedureStepMonitor>))]
        public async Task<IActionResult> UpdateProcedureStepMonitor(UpdateProcedureStepMonitorRequest newproc)
        {
            var command = newproc.ToUpdateProcedureStepMonitorCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ProcedureStepMonitor>();
        }
    }
}
