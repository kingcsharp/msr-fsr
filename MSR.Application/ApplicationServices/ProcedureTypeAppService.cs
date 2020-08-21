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
    public class ProcedureTypeAppService :
        ICommandHandler<GetProcedureType>,
        ICommandHandler<CreateProcedureType>,
        ICommandHandler<DeleteProcedureType>,
        ICommandHandler<UpdateProcedureType>
    {
        private readonly IProcedureTypeService _procedureService;

        public ProcedureTypeAppService(IProcedureTypeService procedureService)
        {
            _procedureService = procedureService;
        }

        public async Task<ICommandResponse> HandleAsync(GetProcedureType command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.GetProcedureTypeAsync(command);
            return new CommandResponse<ICollection<ProcedureType>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreateProcedureType command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.CreateProcedureTypeAsync(command);
            return new CommandResponse<ProcedureType>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(UpdateProcedureType command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.UpdateProcedureTypeAsync(command);
            return new CommandResponse<ProcedureType>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeleteProcedureType command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.DeleteProcedureTypeAsync(command);
            return new CommandResponse<bool>(ret);
        }
    }
}
