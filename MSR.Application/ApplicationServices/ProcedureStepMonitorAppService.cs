using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class ProcedureStepProcedureStepMonitorAppService :
        ICommandHandler<GetProcedureStepProcedureStepMonitor>,
        ICommandHandler<CreateProcedureStepProcedureStepMonitor>,
        ICommandHandler<UpdateProcedureStepProcedureStepMonitor>
    {
        private readonly IProcedureStepProcedureStepMonitorService _procedureService;

        public ProcedureStepProcedureStepMonitorAppService(IProcedureStepProcedureStepMonitorService procedureService)
        {
            _procedureService = procedureService;
        }

        public async Task<ICommandResponse> HandleAsync(GetProcedureStepProcedureStepMonitor command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.GetProcedureStepProcedureStepMonitorAsync(command);
            return new CommandResponse<ICollection<ProcedureStepProcedureStepMonitor>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreateProcedureStepProcedureStepMonitor command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.CreateProcedureStepProcedureStepMonitorAsync(command);
            return new CommandResponse<ProcedureStepProcedureStepMonitor>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(UpdateProcedureStepProcedureStepMonitor command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.UpdateProcedureStepProcedureStepMonitorAsync(command);
            return new CommandResponse<ProcedureStepProcedureStepMonitor>(ret);
        }
    }
}
