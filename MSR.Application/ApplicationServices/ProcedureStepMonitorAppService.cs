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
    public class ProcedureStepMonitorAppService :
        ICommandHandler<GetProcedureStepMonitor>,
        ICommandHandler<GetProcedureStepMonitorDefinition>,
        ICommandHandler<CreateProcedureStepMonitor>,
        ICommandHandler<UpdateProcedureStepMonitor>
    {
        private readonly IProcedureStepMonitorService _procedureService;

        public ProcedureStepMonitorAppService(IProcedureStepMonitorService procedureService)
        {
            _procedureService = procedureService;
        }

        public async Task<ICommandResponse> HandleAsync(GetProcedureStepMonitor command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.GetProcedureStepMonitorAsync(command);
            return new CommandResponse<ICollection<ProcedureStepMonitor>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreateProcedureStepMonitor command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.CreateProcedureStepMonitorAsync(command);
            return new CommandResponse<ProcedureStepMonitor>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(UpdateProcedureStepMonitor command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.UpdateProcedureStepMonitorAsync(command);
            return new CommandResponse<ProcedureStepMonitor>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetProcedureStepMonitorDefinition command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.GetProcedureStepMonitorDefinitionAsync(command);
            return new CommandResponse<ProcedureStepMonitorDefinition>(ret);
        }
    }
}
