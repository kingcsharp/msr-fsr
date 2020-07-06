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
    public class HelpAppService :
        ICommandHandler<CreateHelpPage>,
        ICommandHandler<CreateHelpPageRole>,
        ICommandHandler<UpdateHelpPage>,
        ICommandHandler<DeleteHelpPage>,
        ICommandHandler<DeleteHelpPageRole>,
        ICommandHandler<GetHelpPage>
    {
        IHelpService _helpService;

        public HelpAppService(IHelpService helpService)
        {
            _helpService = helpService;

        }
        public async Task<ICommandResponse> HandleAsync(CreateHelpPage command, CancellationToken cancellationToken = default)
        {
            await _helpService.CreateHelpPage(command);
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(CreateHelpPageRole command, CancellationToken cancellationToken = default)
        {
            await _helpService.CreateHelpPageRole(command);
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(UpdateHelpPage command, CancellationToken cancellationToken = default)
        {
            await _helpService.UpdateHelpPage(command);
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(DeleteHelpPage command, CancellationToken cancellationToken = default)
        {
            await _helpService.DeleteHelpPage(command);
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(DeleteHelpPageRole command, CancellationToken cancellationToken = default)
        {
            await _helpService.DeleteHelpPageRole(command);
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(GetHelpPage command, CancellationToken cancellationToken = default)
        {
            var ret = await _helpService.GetHelpPages(command);
            return new CommandResponse<IEnumerable<HelpPage>>(ret);
        }
    }
}
