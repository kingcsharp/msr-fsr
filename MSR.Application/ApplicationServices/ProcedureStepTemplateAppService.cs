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
        ICommandHandler<DeleteProcedureStepTemplate>,
        ICommandHandler<CreateProcedureStepTemplate>,
        ICommandHandler<UpdateProcedureStepTemplate>
    {
        /// <summary>
        /// Pointer to ProcedureStepTemplateServices
        /// </summary>
        /// <description>
        /// NOTE: procedure template and procedure STEP template are the same
        /// thing.  In the API it is "procedure template," in the database
        /// it is "procedure step template."
        /// </description>
        private readonly IProcedureStepTemplateService _procedureService;
        private readonly IFileService _fileService;

        public ProcedureStepTemplateAppService(IProcedureStepTemplateService procedureService, IFileService _fileService)
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

        public async Task<ICommandResponse> HandleAsync(DeleteProcedureStepTemplate command, CancellationToken cancellationToken = default)
        {
            var ret = await _procedureService.DeleteProcedureStepTemplateAsync(command);
            return new CommandResponse<bool>(ret);
        }
    }
}
