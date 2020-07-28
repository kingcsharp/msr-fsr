using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
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

            ICollection<FileModel> files = new List<FileModel>();

            if (!command.attachFiles)
            {
                files = _fileService.ListFilesForEntitySet(new Part().GetType().Name, ret.Select(x => x.Id).ToList());
            }

            foreach (var part in ret)
            {
                if (command.attachFiles) {
                    files = _fileService.ListFiles(part, part.Id);
                }
                part.Files = files.Where(x => x.EntityId == part.Id).ToList();
            }

            return new CommandResponse<ICollection<PartModel>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreatePart command, CancellationToken cancellationToken = default)
        {
            var ret = await _partService.CreatePartAsync(command);
            var part = ret;

            // If the part is pending approval, do not upload the files yet.
            if (!part.IsPending)
            {
                await _fileService.DeleteFilesAsync(part, part.Id);
                foreach (var file in command.Files)
                {
                    await _fileService.CreateFileAsync(part, part.Id, file);
                }
            }
            return new CommandResponse<PartModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdatePart command, CancellationToken cancellationToken = default)
        {
            var ret = await _partService.UpdatePartAsync(command);
            var part = ret;
            if (!part.IsPending && command.Files != null)
            {
                await _fileService.DeleteFilesAsync(part, part.Id);
                foreach (var file in command.Files)
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
