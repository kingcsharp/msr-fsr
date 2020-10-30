using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Part
{
    public class ProcedureStepTemplateService : IProcedureStepTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IUploadFiles _fileUploader;
        private readonly IDownloadFiles _fileDownloader;

        public ProcedureStepTemplateService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IFileHandlerFactory fileHanderFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;

            _fileUploader = fileHanderFactory.CreateUploader(FileProvider.S3);
            _fileDownloader = fileHanderFactory.CreateDownloader(FileProvider.S3);
        }

        public async Task<ICollection<Domain.Models.ProcedureStepTemplateModel>> GetProcedureStepTemplateAsync(GetProcedureStepTemplate command)
        {
            List<EntityFramework.Entities.ProcedureStepTemplate> procedures;
            List<Domain.Models.ProcedureStepTemplateModel> result;

            if (command.Id.HasValue) {
                procedures = await _unitOfWork.ProcedureStepTemplates
                    .Query()
                    .Where(x => x.Id == command.Id.Value)
                    .Include(x => x.ReferenceFiles)
                    .ThenInclude(x => x.FileObject)
                    .ToListAsync();
                if (procedures.Count == 0) {
                    throw new DomainException($"procedure ID {command.Id.Value} not found", DomainError.NotFound);
                }
            } else {
                procedures = await _unitOfWork.ProcedureStepTemplates
                    .Query()
                    .Include(x => x.ReferenceFiles)
                    .ThenInclude(y => y.FileObject)
                    .ToListAsync();
            }

            // map and attach the right files for this object, if any
            result = procedures.Select(x => {
                var model = _mapper.Map<Domain.Models.ProcedureStepTemplateModel>(x);
                model.ReferenceFiles = new List<FileModel>();
                foreach (FileEntityMap map in x.ReferenceFiles) {
                    if (map.EntityTableName != nameof(EntityFramework.Entities.ProcedureStepTemplate)) {
                        continue;
                    }
                    model.ReferenceFiles.Add(
                        _mapper.Map<FileModel>(map.FileObject)
                    );
                }
                return model;
            }).OrderBy(x => x.Id).ToList();

            return result;
        }

        public async Task<Domain.Models.ProcedureStepTemplateModel> CreateProcedureStepTemplateAsync(CreateProcedureStepTemplate command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepTemplateModel ret;

            if (user.CanApprove(EnumMenuItem.Templates))
            {
                EntityFramework.Entities.ProcedureStepTemplate procedure = _mapper.Map<EntityFramework.Entities.ProcedureStepTemplate>(command);
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                _unitOfWork.ProcedureStepTemplates.Add(procedure);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStepTemplateModel>(procedure);

                var fileReferences = new List<FileModel>();

                foreach (var fileId in command.ReferenceFileIds)
                {
                    var fileModel = await _fileService.MapUploadedFileAsync(nameof(EntityFramework.Entities.ProcedureStepTemplate), ret.Id, fileId);

                    fileReferences.Add(fileModel);
                }

                foreach (var file in command.ReferenceFiles)
                {
                    var fileModel = await _fileService.CreateFileAsync(nameof(EntityFramework.Entities.ProcedureStepTemplate), ret.Id, file);

                    fileReferences.Add(fileModel);
                }

                ret.ReferenceFiles = fileReferences;
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureStepTemplateModel)} uid {user.Id}");
            }

            return ret;
        }
        public async Task<Domain.Models.ProcedureStepTemplateModel> UpdateProcedureStepTemplateAsync(UpdateProcedureStepTemplate command)
        {
            var current = await _unitOfWork.ProcedureStepTemplates.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if (current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureStepTemplate)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepTemplateModel ret;

            if (user.CanApprove(EnumMenuItem.Templates))
            {
                var procedure = _mapper.Map(command, current);
                _unitOfWork.ProcedureStepTemplates.Update(procedure);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                ret = _mapper.Map<Domain.Models.ProcedureStepTemplateModel>(procedure);

                // Update ProcedureStepTemplate files mapping
                var currentFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.ProcedureStepTemplate), command.Id);

                var currentFileIds = currentFiles.Select(i => i.FileId).ToList();
                var fileIdsToAdd = command.ReferenceFileIds.Where(i => !currentFileIds.Contains(i)).ToList();
                var fileIdsToRemove = currentFileIds.Where(i => !command.ReferenceFileIds.Contains(i.Value)).ToList();

                var fileReferences = new List<FileModel>();

                foreach (var addFileId in fileIdsToAdd)
                {
                    var fileModel = await _fileService.MapUploadedFileAsync(nameof(EntityFramework.Entities.ProcedureStepTemplate), command.Id, addFileId);
                }

                foreach (var removeFileId in fileIdsToRemove)
                {
                    await _fileService.DetachFilesAsync(nameof(EntityFramework.Entities.ProcedureStepTemplate), command.Id, removeFileId);
                }

                fileReferences.AddRange(_fileService.ListFiles(nameof(EntityFramework.Entities.ProcedureStepTemplate), command.Id));

                foreach (var file in command.ReferenceFiles)
                {
                    var fileModel = await _fileService.CreateFileAsync(nameof(EntityFramework.Entities.ProcedureStepTemplate), ret.Id, file);

                    fileReferences.Add(fileModel);
                }

                ret.ReferenceFiles = fileReferences;
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureStepTemplateModel)} uid {user.Id}");
            }

            return ret;

        }

        public async Task DeleteProcedureStepTemplateAsync(DeleteProcedureStepTemplate command)
        {
            var current = await _unitOfWork.ProcedureStepTemplates
                .Query()
                .Include(x => x.ReferenceFiles)
                .ThenInclude(y => y.FileObject)
                .FirstOrDefaultAsync(i => i.Id == command.Id);

            if(current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.ProcedureStepTemplate)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            if (CurrentUser.HasPrivilege(EnumMenuItem.Templates, EnumPrivilege.CanDelete)) {
               
                foreach (FileEntityMap map in current.ReferenceFiles)
                {
                    if (map.EntityTableName != nameof(EntityFramework.Entities.ProcedureStepTemplate))
                    {
                        continue;
                    }
                    _unitOfWork.FileEntityMap.Delete(false, map);
                }

                _unitOfWork.ProcedureStepTemplates.Delete(false, current);
                
            } else {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureStepTemplateModel)} uid {CurrentUser.GetId()}");
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
