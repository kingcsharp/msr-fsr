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
    public class PartAppService :
        ICommandHandler<GetParts>,
        ICommandHandler<CreatePart>,
        ICommandHandler<DeletePart>,
        ICommandHandler<UpdatePart>
    {
        private readonly IPartService _partService;
        private readonly IFileService _fileService;

        public PartAppService(IPartService partService, IFileService fileService)
        {
            _partService = partService;
            _fileService = fileService;
        }

        public async Task<ICommandResponse> HandleAsync(GetParts command, CancellationToken cancellationToken = default)
        {
            ICollection<PartModel> ret = await _partService.GetPartsAsync(command);
            return new CommandResponse<ICollection<PartModel>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreatePart command, CancellationToken cancellationToken = default)
        {
            var ret = await _partService.CreatePartAsync(command);
            return new CommandResponse<PartModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdatePart command, CancellationToken cancellationToken = default)
        {
            var ret = await _partService.UpdatePartAsync(command);
            return new CommandResponse<PartModel>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(DeletePart command, CancellationToken cancellationToken = default)
        {
            var ret = await _partService.DeletePartAsync(command);
            return new CommandResponse<PartModel>(ret);
        }
    }
}
