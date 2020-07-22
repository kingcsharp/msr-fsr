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
            foreach (var m in ret) {
                m.Files = _fileService.ListFiles(m, m.Id);

            }
            return new CommandResponse<ICollection<PartModel>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreatePart command, CancellationToken cancellationToken = default)
        {
            var ret = await _partService.CreatePartAsync(command);
            var part = ret;
            await _fileService.DeleteFilesAsync(part, part.Id);
            foreach(var file in command.Files)
            {
                await _fileService.CreateFileAsync(part, part.Id, file);
            }
            return new CommandResponse<PartModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdatePart command, CancellationToken cancellationToken = default)
        {
            var ret = await _partService.UpdatePartAsync(command);
            var part = ret;
            if (command.Files != null) {
                await _fileService.DeleteFilesAsync(part, part.Id);
                foreach(var file in command.Files)
                {
                    await _fileService.CreateFileAsync(part, part.Id, file);
                }
            }
            return new CommandResponse<PartModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeletePart command, CancellationToken cancellationToken = default)
        {
            var ret = await _partService.DeletePartAsync(command);
            return new CommandResponse<PartModel>(ret);
        }
    }
}
