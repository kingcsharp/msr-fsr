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

            var files = _fileService.ListFilesForEntitySet(new Part().GetType().Name, ret.Select(x => x.Id).ToList());

            foreach (var part in ret)
            {
                part.Files = files.Where(x => x.EntityId == part.Id).ToList();
            }

            return new CommandResponse<ICollection<PartModel>>(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreatePart command, CancellationToken cancellationToken = default)
        {
            var part = await _partService.CreatePartAsync(command);

            // If the part is pending approval, do not upload the files yet.
            if (!part.IsPending)
            {
                part.Files = await _fileService.AttachFilesAsync(part.GetType().Name, part.Id, command.Files);
            }
            return new CommandResponse<PartModel>(part);
        }

        public async Task<ICommandResponse> HandleAsync(UpdatePart command, CancellationToken cancellationToken = default)
        {
            var part = await _partService.UpdatePartAsync(command);
            if (!part.IsPending && command.Files != null && command.Files.Count > 0)
            {
                part.Files = await _fileService.AttachFilesAsync(part.GetType().Name, part.Id, command.Files);
            }
            else
            {
                List<int> ids = new List<int>();
                ids.Add(part.Id);
                part.Files = _fileService.ListFilesForEntitySet(new Part().GetType().Name, ids).ToList();
            }
            return new CommandResponse<PartModel>(part);
        }

        public async Task<ICommandResponse> HandleAsync(DeletePart command, CancellationToken cancellationToken = default)
        {
            var ret = await _partService.DeletePartAsync(command);
            return new CommandResponse<PartModel>(ret);
        }
    }
}
