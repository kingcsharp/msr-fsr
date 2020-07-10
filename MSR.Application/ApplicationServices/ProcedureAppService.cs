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
    public class ProcedureAppService :
        ICommandHandler<GetProcedure>,
        ICommandHandler<CreateProcedure>,
        ICommandHandler<UpdateProcedure>
    {
        private readonly IProcedureService _procedureService;

        public ProcedureAppService(IProcedureService procedureService)
        {
            _procedureService = procedureService;
        }

        public async Task<ICommandResponse> HandleAsync(GetProcedure command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.GetProcedureAsync(command);
            return new CommandResponse<ICollection<Procedure>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreateProcedure command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.CreateProcedureAsync(command);
            return new CommandResponse<Procedure>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(UpdateProcedure command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.UpdateProcedureAsync(command);
            return new CommandResponse<Procedure>(ret);
        }
    }
}
