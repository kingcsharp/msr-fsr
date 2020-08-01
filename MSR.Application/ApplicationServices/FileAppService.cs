using AutoMapper;
using MSR.Answer.Domain.Models;
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
    public class FileAppService :
        ICommandHandler<GetFiles>,
        ICommandHandler<CreateFile>,
        ICommandHandler<DetachFile>,
        ICommandHandler<UploadFile>
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FileAppService(IFileService fileService, IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        public Task<ICommandResponse> HandleAsync(GetFiles command, CancellationToken cancellationToken = default)
        {
            ICollection<FileModel> files = _fileService.ListFiles(
                command.entityName, command.entityId, command.fileId
            );

            ICommandResponse ret = new CommandResponse<ICollection<FileModel>>(files);
            return Task.FromResult(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreateFile command, CancellationToken cancellationToken = default)
        {
            var file = _mapper.Map<FileModel>(command);
            var ret = await _fileService.CreateFileAsync(command.EntityName, command.EntityId, file);
            return new CommandResponse<FileModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DetachFile command, CancellationToken cancellationToken = default)
        {
            var ret = await _fileService.DetachFilesAsync(command.entityName, command.entityId, command.fileId);
            return new CommandResponse<int>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UploadFile command, CancellationToken cancellationToken = default)
        {
            var ret = await _fileService.UploadHelpFile(command);
            return new CommandResponse<UploadResponse>(ret);

        }
    }
}
