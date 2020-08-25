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
    public class ProcedureStepTemplateAppService :
        ICommandHandler<GetProcedureStepTemplate>,
        ICommandHandler<CreateProcedureStepTemplate>,
        ICommandHandler<UpdateProcedureStepTemplate>
    {
        private readonly IProcedureStepTemplateService _procedureService;

        public ProcedureStepTemplateAppService(IProcedureStepTemplateService procedureService)
        {
            _procedureService = procedureService;
        }

        public async Task<ICommandResponse> HandleAsync(GetProcedureStepTemplate command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.GetProcedureStepTemplateAsync(command);
            return new CommandResponse<ICollection<ProcedureStepTemplateModel>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreateProcedureStepTemplate command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.CreateProcedureStepTemplateAsync(command);
            return new CommandResponse<ProcedureStepTemplateModel>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(UpdateProcedureStepTemplate command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.UpdateProcedureStepTemplateAsync(command);
            return new CommandResponse<ProcedureStepTemplateModel>(ret);
        }
    }
}
