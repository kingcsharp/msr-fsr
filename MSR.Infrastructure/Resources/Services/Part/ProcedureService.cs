using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Validators;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System;
using MSR.Infrastructure.Resources.Queries;
using MSR.Infrastructure.Resources.EntityFramework.Projections;
using MSR.Domain.Views;

namespace MSR.Infrastructure.Resources.Services.Part
{
    // Helper class to hold import data that doesn't
    // directly map to Answer 3.0 classes
    class CreateProcedureStepImport
    {
        public string COMMENT { get; set; }
        public string EXTRA_NOTE1 { get; set; }
        public string SERIALIZE { get; set; }
        public string SUCCESS_MONITOR { get; set; }
        public string INTERNAL_LOCATION { get; set; }
        public string LOC_TYPE { get; set; }
        public string REF_DOC_ID { get; set; }
    }

    // Helper class to hold import data that doesn't
    // directly map to Answer 3.0 classes
    class CreateProcedureImport
    {
        public string ANS_ID { get; set; }
    }

    class ParsedProcedureImport
    {
        public Dictionary<string, CreateProcedure> Procedures { get; set; }
        public Dictionary<string, CreateProcedureImport> ProcedureExtras { get; set; }
        public Dictionary<string, List<CreateProcedureStep>> ProcedureSteps { get; set; }
        public Dictionary<string, List<CreateProcedureStepImport>> ProcedureStepExtras { get; set; }
    }

    public class ProcedureService : IProcedureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ProcedureValidator _validator;
        private readonly IMessageHubClient _messageHub;

        public ProcedureService(IUnitOfWork unitOfWork,
            IMapper mapper, IFileService fileService, ProcedureValidator validator,
            IMessageHubClient messageHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _validator = validator; // used for import
            _messageHub = messageHub;
        }

        public async Task<ICollection<Domain.Models.Procedure>> GetProcedureAsync(GetProcedure command)
        {
            if (command.Id.HasValue)
            {
                return await GetSingleProcedureAsync(command);
            }
            else
            {
                return await GetAllProceduresAsync();
            }
        }

        public async Task<Domain.Models.Procedure> CreateProcedureAsync(CreateProcedure command, bool isImport = false)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Procedure procedureModel;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval) || isImport)
            {
                var procedureEntity = _mapper.Map<EntityFramework.Entities.Procedure>(command);
                procedureEntity.Revision = 1;
                var created = _unitOfWork.Procedures.Add(procedureEntity);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(procedureEntity, procedureEntity.Id, "Approved", command.Comments);

                // The API return expects the procedure type object to be loaded.
                created.Context.Entry(procedureEntity).Reference(x => x.ProcedureType).Load();

                var fileReferences = new List<FileModel>();
                if (command.ReferenceFiles?.Count > 0)
                {
                    foreach (FileModel file in command.ReferenceFiles)
                    {
                        FileModel newFile = await _fileService.CreateFileAsync(
                            nameof(EntityFramework.Entities.Procedure),
                            procedureEntity.Id,
                            file
                        );
                    }
                }
                if (command.ReferenceFileIds?.Count > 0)
                {
                    foreach (var commandReferenceFileId in command.ReferenceFileIds)
                    {
                        await _fileService.MapUploadedFileAsync(nameof(EntityFramework.Entities.Procedure), procedureEntity.Id, commandReferenceFileId);
                    }
                }
                fileReferences.AddRange(_fileService.ListFiles(nameof(EntityFramework.Entities.Procedure), procedureEntity.Id));

                procedureModel = _mapper.Map<Domain.Models.Procedure>(procedureEntity);
                procedureModel.ReferenceFiles = fileReferences;
            }
            else
            {
                var approval = _mapper.Map<ProcedureApproval>(command);
                approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
                approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
                approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
                _unitOfWork.ProcedureApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                procedureModel = _mapper.Map<Domain.Models.Procedure>(approval);
                _messageHub.SendApprovalNotification(EnumApprovalTables.ProcedureApproval);
            }

            return procedureModel;
        }

        public async Task<Domain.Models.Procedure> CopyProcedureAsync(CopyProcedure command)
        {
            if (!CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval))
            {
                throw new DomainException($"Permission denied to create procedures", DomainError.BadRequest);
            }

            EntityFramework.Entities.Procedure procedure = await _unitOfWork.Procedures
                .Query()
                .Where(i => i.Id == command.SourceProcedureId)
                .Include(x => x.ProcedureSteps)
                    .ThenInclude(y => y.ReferenceFiles)
                .Include(x => x.ProcedureSteps)
                    .ThenInclude(y => y.ProcedureStepRoles)
                .Include(x => x.ProcedureSteps)
                    .ThenInclude(y => y.ProcedureStepMonitors)
                .Include(x => x.ReferenceFiles)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (procedure is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Procedure)} not found with ID: {command.SourceProcedureId}", DomainError.NotFound);
            }

            var newProcedure = new EntityFramework.Entities.Procedure();
            _mapper.Map(procedure, newProcedure);
            newProcedure.Id = 0;
            newProcedure.Name = procedure.Name + " - Copy";
            newProcedure.Revision = 1;

            Dictionary<int, List<DocumentEntityMap>> documents =
                new Dictionary<int, List<DocumentEntityMap>>();

            foreach (var newStep in newProcedure.ProcedureSteps)
            {
                int originalProcedureStepId = newStep.Id;
                newStep.OldId = originalProcedureStepId;
                newStep.Id = 0;
                newStep.ProcedureId = 0;
                foreach (var newRole in newStep.ProcedureStepRoles)
                {
                    newRole.Id = 0;
                    newRole.ProcedureStepId = 0;
                }
                foreach (var newFile in newStep.ReferenceFiles)
                {
                    newFile.Id = 0;
                    newFile.EntityId = 0;
                }
                foreach (var newMonitor in newStep.ProcedureStepMonitors)
                {
                    newMonitor.Id = 0;
                    newMonitor.ProcedureStepId = 0;
                }

                List<DocumentEntityMap> documentMaps = await _unitOfWork.DocumentEntityMap
                    .Query()
                    .Where(x => x.EntityTableName.Equals(nameof(ProcedureStep)) &&
                           x.EntityId == originalProcedureStepId)
                    .AsNoTracking()
                    .ToListAsync();
                documents.Add(originalProcedureStepId, documentMaps);
            }
            newProcedure.ReferenceFiles = new List<FileEntityMap>();
            foreach (var file in procedure.ReferenceFiles)
            {
                if (!file.EntityTableName.Equals(nameof(EntityFramework.Entities.Procedure)))
                {
                    continue;
                }
                var newFile = _mapper.Map<FileEntityMap>(file);
                newFile.Id = 0;
                newFile.EntityId = 0;
                newProcedure.ReferenceFiles.Add(newFile);
            }

            // user has approval permission, create the record
            _unitOfWork.Procedures.Add(newProcedure);

            await _unitOfWork.LogApprovalTransaction(newProcedure, newProcedure.Id);

            // The steps must be created before the documents can be linked (if any).
            newProcedure.ProcedureSteps
                .Where(x => x.OldId.HasValue && documents.ContainsKey(x.OldId.Value))
                .ToList()
                .ForEach(step =>
                {
                    List<DocumentEntityMap> stepDocuments =
                        documents.GetValueOrDefault(step.OldId.Value);
                    stepDocuments.ForEach(stepDocument =>
                    {
                        stepDocument.EntityId = step.Id;
                        stepDocument.Id = 0;
                        stepDocument.Created = null;
                        stepDocument.CreatedBy = 0;
                        _unitOfWork.DocumentEntityMap.Add(stepDocument);
                    });
                });
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Domain.Models.Procedure>(newProcedure);
        }

        public async Task<Domain.Models.Procedure> UpdateProcedureAsync(UpdateProcedure command, bool isImport = false)
        {
            var current = await _unitOfWork.Procedures
                .Query()
                .Where(i => i.Id == command.Id)
                .Include(x => x.ProcedureType)
                .FirstOrDefaultAsync();

            if (current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Procedure)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Procedure procedureModel;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval) || isImport)
            {
                int revision = current.Revision;
                var procedure = _mapper.Map(command, current);
                procedure.Revision = revision + 1;
                _unitOfWork.Procedures.Update(procedure);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id, "Approved", command.Comments);

                procedureModel = _mapper.Map<Domain.Models.Procedure>(procedure);

                // Update Reference Files mapping
                var currentFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.Procedure), command.Id);
                var currentFileIds = currentFiles.Select(i => i.FileId).ToList();
                List<int> fileIdsToAdd;
                List<int> fileIdsToRemove;

                if (command.ReferenceFileIds == null)
                {
                    // if the list of IDs in the comand is null, then
                    // we assue that we're not supposed to change the list.
                    fileIdsToAdd = new List<int>();
                    fileIdsToRemove = new List<int>();
                }
                else
                {
                    // add the files in the command but not in the current list
                    fileIdsToAdd = command.ReferenceFileIds
                        .Where(i => !currentFileIds.Contains(i))
                        .ToList();
                    // remove the files in the list but not in the command
                    fileIdsToRemove = currentFileIds
                        .Where(i => i.HasValue && !command.ReferenceFileIds.Contains(i.Value))
                        .Select(i => i.Value)
                        .ToList();
                }

                var fileReferences = new List<FileModel>();

                foreach (var addFileId in fileIdsToAdd)
                {
                    await _fileService.MapUploadedFileAsync(nameof(EntityFramework.Entities.Procedure), command.Id, addFileId);
                }

                foreach (var removeFileId in fileIdsToRemove)
                {
                    await _fileService.DetachFilesAsync(nameof(EntityFramework.Entities.Procedure), command.Id, removeFileId);
                }

                fileReferences.AddRange(_fileService.ListFiles(nameof(EntityFramework.Entities.Procedure), command.Id));

                if (command.ReferenceFiles != null)
                {
                    foreach (var file in command.ReferenceFiles)
                    {
                        var fileModel = await _fileService.CreateFileAsync(nameof(EntityFramework.Entities.Procedure), procedureModel.Id, file);

                        fileReferences.Add(fileModel);
                    }
                }

                procedureModel.ReferenceFiles = fileReferences;
            }
            else
            {
                var approval = _mapper.Map<ProcedureApproval>(command);
                approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
                approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
                approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
                _unitOfWork.ProcedureApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                procedureModel = _mapper.Map<Domain.Models.Procedure>(approval);
                _messageHub.SendApprovalNotification(EnumApprovalTables.ProcedureApproval);
            }

            return procedureModel;

        }

        public async Task<ICollection<ProcedureStepModel>> GetProcedureStepAsync(GetProcedureStep command)
        {
            List<ProcedureStep> steps;

            var query = _unitOfWork.ProcedureSteps
                .Query()
                .Include(x => x.StepType)
                .Include(x => x.ProcedureStepRoles)
                .ThenInclude(y => y.Role);

            if (command.stepId.HasValue)
            {
                steps = await query
                    .Where(x => x.Id == command.stepId.Value)
                    .ToListAsync();
                if (steps.Count == 0)
                {
                    throw new DomainException($"step ID {command.stepId.Value} not found", DomainError.NotFound);
                }
            }
            else
            {
                steps = await query
                    .Where(x => x.ProcedureId == command.procedureId)
                    .ToListAsync();
            }

            var procedureStepModels = steps.Select(x => _mapper.Map<ProcedureStepModel>(x)).OrderBy(x => x.PrintOrder).ToList();

            var procedureStepIds = steps.Select(m => m.Id).ToList();

            var documentEntityMaps = await _unitOfWork.DocumentEntityMap.Query().Where(s =>
                procedureStepIds.Contains(s.EntityId) &&
                s.EntityTableName == nameof(ProcedureStep)).ToListAsync();

            var monitors = await _unitOfWork.ProcedureStepMonitors.Query().Where(i => procedureStepIds.Contains((int)i.ProcedureStepId)).ToListAsync();

            var workOrderTasksInUse = _unitOfWork.WorkOrderTasks.Query()
                .Where(s => procedureStepIds.Contains(s.ProcedureStepId.Value)
                            && (s.StatusId == (int)EnumStatusSteps.InProgress
                               || s.StatusId == (int)EnumStatusSteps.WaitingtoStart
                               || s.StatusId == (int)EnumStatusSteps.Approved));

            procedureStepModels.ForEach(procedureStep =>
            {
                procedureStep.ReferenceFiles = _fileService.ListFiles(nameof(ProcedureStep), procedureStep.Id).ToList();
                procedureStep.ReferenceDocumentIds = documentEntityMaps.Where(s => s.EntityId == procedureStep.Id).Select(m => m.DocumentId).ToList();
                procedureStep.IsUsed = workOrderTasksInUse.Any(s => s.ProcedureStepId == procedureStep.Id);
                procedureStep.ProcedureStepMonitors = monitors.Where(i => i.ProcedureStepId == procedureStep.Id).Select(i => _mapper.Map<Domain.Models.ProcedureStepMonitor>(i)).ToList();
            });

            return procedureStepModels;
        }

        public async Task<ProcedureStepModel> CreateProcedureStepAsync(CreateProcedureStep command, bool isImport = false)
        {

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval) || isImport)
            {
                var procedureStepEntity = _mapper.Map<ProcedureStep>(command);
                await _unitOfWork.ProcedureSteps.AddAsync(procedureStepEntity);
                await _unitOfWork.LogApprovalTransaction(procedureStepEntity, procedureStepEntity.Id);

                var fileReferences = new List<FileModel>();
                if (command.ReferenceFiles?.Count > 0)
                {
                    foreach (FileModel file in command.ReferenceFiles)
                    {
                        FileModel newFile = await _fileService.CreateFileAsync(
                            nameof(EntityFramework.Entities.ProcedureStep),
                            procedureStepEntity.Id,
                            file
                        );
                    }
                }
                if (command.ReferenceFileIds?.Count > 0)
                {
                    foreach (var commandReferenceFileId in command.ReferenceFileIds)
                    {
                        await _fileService.MapUploadedFileAsync(nameof(EntityFramework.Entities.ProcedureStep), procedureStepEntity.Id, commandReferenceFileId);
                    }
                }
                fileReferences.AddRange(_fileService.ListFiles(nameof(EntityFramework.Entities.ProcedureStep), procedureStepEntity.Id));

                var roleModels = new List<Domain.Models.Role>();
                if (procedureStepEntity.ProcedureStepRoles != null)
                {
                    foreach (ProcedureStepRoleMap m in procedureStepEntity.ProcedureStepRoles)
                    {
                        m.ProcedureStepId = procedureStepEntity.Id;
                        await _unitOfWork.ProcedureStepRoleMaps.AddAsync(m);
                        roleModels.Add(_mapper.Map<Domain.Models.Role>(m.Role));
                    }
                }

                var referenceDocumentIds = new List<int>();
                if (command.ReferenceDocumentIds != null)
                {
                    foreach (int newDocId in command.ReferenceDocumentIds)
                    {
                        var documentEntityMap = new DocumentEntityMap()
                        {
                            EntityId = procedureStepEntity.Id,
                            EntityTableName = nameof(ProcedureStep),
                            DocumentId = newDocId
                        };
                        await _unitOfWork.DocumentEntityMap.AddAsync(documentEntityMap);
                    }

                    referenceDocumentIds = await _unitOfWork.DocumentEntityMap
                        .Query()
                        .Where(x => x.EntityTableName == nameof(ProcedureStep) && x.EntityId == procedureStepEntity.Id)
                        .Select(x => x.DocumentId)
                        .ToListAsync();
                }


                var procedureStepModel = _mapper.Map<ProcedureStepModel>(procedureStepEntity);
                procedureStepModel.Roles = roleModels;
                procedureStepModel.ReferenceFiles = fileReferences;
                procedureStepModel.ReferenceDocumentIds = referenceDocumentIds;
                return procedureStepModel;
            }

            var currentProcedure = await _unitOfWork.Procedures
                .Query()
                .FirstAsync(x => x.Id == command.procedureId);

            int procApprovalId = await FlagProcedureForApproval(currentProcedure, command.procedureId);

            var procedureStepApprovalEntity = _mapper.Map<ProcedureStepApproval>(command);
            procedureStepApprovalEntity.ProcedureApprovalId = procApprovalId;
            await _unitOfWork.ProcedureStepApprovals.AddAsync(procedureStepApprovalEntity);
            await _unitOfWork.SaveChangesAsync();

            _messageHub.SendApprovalNotification(EnumApprovalTables.ProcedureApproval);
            return _mapper.Map<Domain.Models.ProcedureStepModel>(procedureStepApprovalEntity);
        }

        public async Task<ProcedureStepModel> UpdateProcedureStepAsync(UpdateProcedureStep command, bool incRevision = true, bool isImport = false)
        {
            ProcedureStep originalProcedureStepEntity = await _unitOfWork.ProcedureSteps
                .Query()
                .Include(x => x.StepType)
                .Include(x => x.ReferenceFiles)
                .Include(x => x.Procedure)
                .FirstOrDefaultAsync(i =>
                    i.Id == command.procedureStepId);

            if (originalProcedureStepEntity is null)
            {
                throw new DomainException($"{nameof(ProcedureStep)} not " +
                    $"found with ID: {command.procedureId} / {command.procedureStepId}",
                    DomainError.NotFound);
            }

            _unitOfWork.ProcedureSteps.LoadCollection(originalProcedureStepEntity, "ProcedureStepRoles");

            List<int> idsOfFilesToBeMappedAndSaved = new List<int>();

            // Upload any new files, but do not attach yet.
            if (command.ReferenceFiles?.Count > 0)
            {
                foreach (FileModel file in command.ReferenceFiles)
                {
                    FileModel newFile = await _fileService.CreateFileAsync(
                        nameof(EntityFramework.Entities.ProcedureStep),
                        0, // will be attached after checking perms
                        file
                    );
                    idsOfFilesToBeMappedAndSaved.Add(newFile.FileId.Value);
                }
            }
            if (command.ReferenceFileIds?.Count > 0)
            {
                foreach (var commandReferenceFileId in command.ReferenceFileIds)
                {
                    idsOfFilesToBeMappedAndSaved.Add(commandReferenceFileId);
                }
            }

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval) || isImport)
            {
                var roleMapsToRemoveIds = new List<int>();
                if (command.Roles != null &&
                    command.Roles.Count > 0 &&
                    command.Roles.All(x => x != null))
                {
                    var incomingRoleIds = command.Roles.Select(i => i.Id).ToList();
                    roleMapsToRemoveIds = originalProcedureStepEntity.ProcedureStepRoles.Where(i => !incomingRoleIds.Contains(i.RoleId)).Select(i => i.Id).ToList();
                }
                else
                {
                    roleMapsToRemoveIds = originalProcedureStepEntity.ProcedureStepRoles.Select(i => i.Id).ToList();
                }

                foreach (ProcedureStepRoleMap m in originalProcedureStepEntity.ProcedureStepRoles)
                {
                    if (roleMapsToRemoveIds.Contains(m.Id))
                    {
                        _unitOfWork.ProcedureStepRoleMaps.Delete(false, m);
                    }
                }

                var updatedProcedureStepEntity = _mapper.Map(command, originalProcedureStepEntity);

                await _fileService.DetachFilesAsync(
                    nameof(ProcedureStep),
                    updatedProcedureStepEntity.Id);

                foreach (int fileId in idsOfFilesToBeMappedAndSaved)
                {
                    await _fileService.MapUploadedFileAsync(
                        nameof(ProcedureStep),
                        updatedProcedureStepEntity.Id, fileId
                    );
                }

                List<int> savedDocumentIds = await _unitOfWork.DocumentEntityMap
                    .Query()
                    .Where(x => x.EntityId == updatedProcedureStepEntity.Id &&
                        x.EntityTableName.Equals(nameof(ProcedureStep)))
                    .Select(x => x.Id)
                    .ToListAsync();

                foreach (int documentId in savedDocumentIds)
                {
                    _unitOfWork.DocumentEntityMap.Delete(false, documentId);
                }

                var referenceDocumentIds = new List<int>();
                if (command.ReferenceDocumentIds != null)
                {
                    foreach (int newDocId in command.ReferenceDocumentIds)
                    {
                        var documentEntityMap = new DocumentEntityMap()
                        {
                            EntityId = updatedProcedureStepEntity.Id,
                            EntityTableName = nameof(ProcedureStep),
                            DocumentId = newDocId
                        };
                        await _unitOfWork.DocumentEntityMap.AddAsync(documentEntityMap);
                    }

                    referenceDocumentIds = await _unitOfWork.DocumentEntityMap
                        .Query()
                        .Where(x => x.EntityTableName == nameof(ProcedureStep) && x.EntityId == updatedProcedureStepEntity.Id)
                        .Select(x => x.DocumentId)
                        .ToListAsync();
                }

                if (updatedProcedureStepEntity.ProcedureStepRoles != null)
                {
                    foreach (ProcedureStepRoleMap m in updatedProcedureStepEntity.ProcedureStepRoles)
                    {
                        m.ProcedureStepId = updatedProcedureStepEntity.Id;
                    }
                }

                if (incRevision)
                {
                    updatedProcedureStepEntity.Procedure.Revision += 1;
                }
                _unitOfWork.ProcedureSteps.Update(updatedProcedureStepEntity);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(updatedProcedureStepEntity, updatedProcedureStepEntity.Id);

                var procedureStepModel = _mapper.Map<ProcedureStepModel>(updatedProcedureStepEntity);
                procedureStepModel.ReferenceDocumentIds = referenceDocumentIds;
                return procedureStepModel;
            }

            _unitOfWork.ProcedureSteps.LoadReference(originalProcedureStepEntity, x => x.Procedure);
            int procApprovalId = await FlagProcedureForApproval(
                originalProcedureStepEntity.Procedure, originalProcedureStepEntity.ProcedureId.Value
            );
            var approval = _mapper.Map<ProcedureStepApproval>(command);
            approval.ProcedureApprovalId = procApprovalId;

            string json = JsonConvert.SerializeObject(new
            {
                roleIds = command.Roles.Select(x => x.Id).ToList(),
                fileIds = idsOfFilesToBeMappedAndSaved,
                documentIds = command.ReferenceDocumentIds,
            });
            approval.ApprovalJSON = json;

            _unitOfWork.ProcedureStepApprovals.Add(approval);
            await _unitOfWork.SaveChangesAsync();

            _messageHub.SendApprovalNotification(EnumApprovalTables.ProcedureApproval);
            return _mapper.Map<ProcedureStepModel>(approval);
        }

        public async Task<bool> DeleteProcedureAsync(DeleteProcedure command)
        {
            var current = await _unitOfWork.Procedures.FirstOrDefaultAsync(false, i => i.Id == command.procedureID);
            if (current is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Procedure)} not found with ID: {command.procedureID}", DomainError.NotFound);
            }
            if (CurrentUser.HasPrivilege(EnumMenuItem.RunnableProcedures, EnumPrivilege.CanDelete))
            {
                _unitOfWork.Procedures.LoadCollection(current, "ProcedureSteps");
                var procedureStepIds = current.ProcedureSteps.Select(x => x.Id).ToList();

                _unitOfWork.ProcedureStepRoleMaps
                    .Query()
                    .Where(x => procedureStepIds.Contains(x.ProcedureStepId))
                    .Select(x => x.Id)
                    .ToList()
                    .ForEach(id =>
                    {
                        _unitOfWork.ProcedureStepRoleMaps.Delete(false, id);
                    });

                _unitOfWork.ProcedureStepMonitors
                    .Query()
                    .Where(x => procedureStepIds.Contains(x.ProcedureStepId.Value))
                    .Select(x => x.Id)
                    .ToList()
                    .ForEach(id =>
                    {
                        _unitOfWork.ProcedureStepMonitors.Delete(false, id);
                    });

                procedureStepIds.ForEach(id =>
                {
                    _unitOfWork.ProcedureSteps.Delete(false, id);
                });

                _unitOfWork.FileEntityMap
                    .Query()
                    .Where(x => procedureStepIds.Contains(x.EntityId) && x.EntityTableName.Equals(nameof(ProcedureStep)))
                    .Select(x => x.Id)
                    .ToList()
                    .ForEach(id =>
                    {
                        _unitOfWork.FileEntityMap.Delete(false, id);
                    });

                _unitOfWork.ProcedureStepRoleMaps
                    .Query()
                    .Where(x => procedureStepIds.Contains(x.ProcedureStepId))
                    .Select(x => x.Id)
                    .ToList()
                    .ForEach(id =>
                    {
                        _unitOfWork.ProcedureStepRoleMaps.Delete(false, id);
                    });

                procedureStepIds.ForEach(id =>
                {
                    _unitOfWork.ProcedureSteps.Delete(false, id);
                });

                _unitOfWork.CascadeDelete(current);
                await _unitOfWork.LogApprovalTransaction(current, current.Id, "Deleted");
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.Procedure)} uid {CurrentUser.GetId()}");
            }

            return true;
        }

        public async Task<ProcedureStepModel> DeleteProcedureStepAsync(DeleteProcedureStep command)
        {
            var procedureStepEntity = await _unitOfWork.ProcedureSteps.FirstOrDefaultAsync(false,
                i => i.ProcedureId == command.procedureID && i.Id == command.procedureStepID
            );

            procedureStepEntity.ProcedureStepRoles = await _unitOfWork.ProcedureStepRoleMaps.Query()
                .Where(s => s.ProcedureStepId == command.procedureStepID).ToListAsync();

            procedureStepEntity.ProcedureStepMonitors = await _unitOfWork.ProcedureStepMonitors.Query()
                .Where(s => s.ProcedureStepId == command.procedureStepID).ToListAsync();

            if (procedureStepEntity is null)
            {
                throw new DomainException($"{nameof(ProcedureStep)} not found with ID: {command.procedureID}/{command.procedureStepID}", DomainError.NotFound);
            }
            if (CurrentUser.HasPrivilege(EnumMenuItem.RunnableProcedures, EnumPrivilege.CanDelete))
            {

                await DeleteProcedureStepMonitorsAsync(procedureStepEntity.Id);
                await DeleteProcedureStepsAsync(procedureStepEntity.Id);
                await _unitOfWork.SaveChangesAsync();

            }
            else
            {
                throw new DomainException($"Permission denied for {nameof(Domain.Models.ProcedureStepModel)} uid {CurrentUser.GetId()}");
            }

            var procedureStepModel = _mapper.Map<ProcedureStepModel>(procedureStepEntity);

            return procedureStepModel;
        }

        public async Task<ICollection<Domain.Models.ProcedureStepTypeModel>> GetProcedureStepType(GetProcedureStepType command)
        {
            List<ProcedureStepType> current;

            if (command.Id.HasValue)
            {
                current = await _unitOfWork.ProcedureStepTypes.Query().Where(
                    i => i.Id == command.Id
                ).ToListAsync();
            }
            else
            {
                current = await _unitOfWork.ProcedureStepTypes.Query().ToListAsync();
            }
            if (current is null || current.Count == 0)
            {
                throw new DomainException($"{nameof(ProcedureStepType)} not found with ID: {command.Id}", DomainError.NotFound);
            }
            return current.Select(x => _mapper.Map<Domain.Models.ProcedureStepTypeModel>(x)).ToList();
        }

        // Add the procedure approval record if it doesn't already exist.  Used
        // to wrap up all changes to steps in a single approval on the workflow screen.
        private async Task<int> FlagProcedureForApproval(EntityFramework.Entities.Procedure currentProcedure, int procedureId)
        {
            var existing = _unitOfWork.ProcedureApprovals
                .Query()
                .Where(x => x.ProcedureId == procedureId);
            int procedureApprovalId;

            if (!existing.Any())
            {
                var approval = _mapper.Map<ProcedureApproval>(currentProcedure);
                approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
                approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
                approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                await _unitOfWork.ProcedureApprovals.AddAsync(approval);
                await _unitOfWork.SaveChangesAsync();

                procedureApprovalId = approval.Id;
            }
            else
            {
                procedureApprovalId = existing.FirstOrDefault().Id;

            }

            return procedureApprovalId;
        }

        public async Task<ICollection<Domain.Models.Procedure>> ImportProcedures(byte[] xlsData)
        {
            IEnumerable<ImportError> importErrors;
            List<Domain.Models.Procedure> createdProcs = new List<Domain.Models.Procedure>();

            var import = ParseData(xlsData, out importErrors);
            if (importErrors.Any() || import == null)
            {
                string allErrors = string.Join("\n", importErrors.Select(x =>
                {
                    string errMsg = string.Join(", ", x.Errors);
                    return $"Line {x.Line}: {errMsg}";
                }));
                throw new DomainException(allErrors, DomainError.BadRequest);
            }

            // Iterate through each procedure and add it, adding
            // each procedure's step along the way.
            foreach (var procedure in import)
            {
                var proc = new Domain.Models.Procedure();
                var currentSteps = new List<ProcedureStep>();
                var currentStepIds = new List<int>();
                try
                {
                    
                    if (procedure.Id.HasValue)
                    {
                        //It's existing update it.
                        var updatedProcedureCommand = _mapper.Map<UpdateProcedure>(procedure);
                        proc = await UpdateProcedureAsync(updatedProcedureCommand, true);
                        currentSteps = await _unitOfWork.ProcedureSteps.Query().Where(i => i.ProcedureId == proc.Id).ToListAsync();
                        currentStepIds = currentSteps.Select(i => i.Id).ToList();
                    }
                    else
                    {
                        //It doesn't exist so create it
                        var createProcedureCommand = _mapper.Map<CreateProcedure>(procedure);
                        if(createProcedureCommand.DurationType == null)
                        {
                            createProcedureCommand.DurationType = "hours";
                        }
                        proc = await CreateProcedureAsync(createProcedureCommand, true);
                    }

                    if (procedure.ProcedureSteps != null && procedure.ProcedureSteps.Any())
                    {
                        var incomingStepIds = procedure.ProcedureSteps.Where(i => i.Id.HasValue).Select(i => i.Id.Value).ToList();
                        var stepsToAdd = procedure.ProcedureSteps.Where(i => (i.Id.HasValue && i.Id.Value == 0) || !i.Id.HasValue).ToList();
                        var stepsToRemoveIds = currentStepIds.Where(i => !incomingStepIds.Contains(i)).ToList();
                        var stepsToUpdateIds = incomingStepIds.Where(i => !stepsToRemoveIds.Contains(i)).ToList();

                        var stepsToRemove = currentSteps.Where(i => stepsToRemoveIds.Contains(i.Id));
                        var stepsToUpdate = procedure.ProcedureSteps.Where(i => i.Id.HasValue && stepsToUpdateIds.Contains(i.Id.Value));

                        foreach(var step in stepsToAdd)
                        {
                            var stepToAdd = _mapper.Map<CreateProcedureStep>(step);
                            stepToAdd.procedureId = proc.Id;
                            await CreateProcedureStepAsync(stepToAdd, true);
                        }

                        foreach(var step in stepsToRemove)
                        {
                            await DeleteProcedureStepsAsync(step.Id);
                        }

                        foreach(var step in stepsToUpdate)
                        {
                            var stepToUpdate = _mapper.Map<UpdateProcedureStep>(step);
                            await UpdateProcedureStepAsync(stepToUpdate, isImport: true);
                        }
                    }
                   
                    createdProcs.Add(proc);
                }
                catch (Exception)
                { }
            }

            return createdProcs;
        }

        public async Task<bool> ReorderSteps(ReorderSteps command)
        {
                var procedures = await _unitOfWork.ProcedureSteps.Query().Where(i => i.ProcedureId == command.ProcedureId).ToListAsync();

                foreach (var procedure in procedures)
                {
                    var order = command.ProcedureSteps.FirstOrDefault(i => i.StepId == procedure.Id).PrintOrder;

                    procedure.PrintOrder = order;
                    _unitOfWork.ProcedureSteps.Update(procedure);
                }

                await _unitOfWork.SaveChangesAsync();

                return true;
        }

        private List<ProcedureImportItem> ParseData(byte[] binData, out IEnumerable<ImportError> importErrors)
        {
            var result = XLSHelper.ParseRecords(binData);
            var returnedProcedureData = ConvertProcedureData(result.Tables);

            importErrors = returnedProcedureData.errors;
            return returnedProcedureData.importItems;
        }

        private (List<ImportError> errors, List<ProcedureImportItem> importItems) ConvertProcedureData(DataTableCollection importData)
        {
            int lineNumber = 1;
            var errors = new List<ImportError>();

            var procedureTypeIds = _unitOfWork.ProcedureTypes.Query().Select(i => i.Id).ToList();
            var procedureIds = _unitOfWork.Procedures.Query().Select(i => i.Id).ToList();

            var procedures = importData[0];
            var procedureSteps = importData[1];
            var procedureImportItems = procedures.ToList<ProcedureImportItem>();
            var procedureStepImportItems = procedureSteps.ToList<ProcedureStepImportItem>();
            var validImportItems = new List<ProcedureImportItem>();
            var allRoleIds = _unitOfWork.Roles.Query().Select(i => i.Id).ToList();

            foreach (var procedureImportItem in procedureImportItems)
            {
                try
                {
                    if (procedureImportItem.Id.HasValue && procedureImportItem.Id.Value > 0
                        && !procedureIds.Contains(procedureImportItem.Id.Value))
                    {
                        errors.Add(new ImportError()
                        {
                            Errors = new List<string>()
                            {
                                $"Procedure with Id: {procedureImportItem.Id} Does Not Exist"
                            },
                            Line = lineNumber
                        });
                    }

                    if (string.IsNullOrWhiteSpace(procedureImportItem.Name))
                    {
                        errors.Add(new ImportError()
                        {
                            Errors = new List<string>
                            {
                                $"Procedure with Id: {procedureImportItem.Id} and Name: {procedureImportItem.Name} is missing Required Procedure Name"
                            },
                            Line = lineNumber
                        });
                    }

                    if (!procedureTypeIds.Contains(procedureImportItem.ProcedureType))
                    {
                        errors.Add(new ImportError()
                        {
                            Errors = new List<string>()
                            {
                                $"Procedure with Id: {procedureImportItem.Id} and Name: {procedureImportItem.Name} has an Invalid ProcedureType Id: {procedureImportItem.ProcedureType}"
                            },
                            Line = lineNumber
                        });
                    }

                    //Now that we're adding in the Name to all we want to only check the name if the Id isn't present so that we don't accidently pull wrong steps.
                    var procedureStepItems = procedureStepImportItems.Where(i => i.ProcedureId == procedureImportItem.Id || (i.ProcedureId == 0 && i.ProcedureName == procedureImportItem.Name)).ToList();
                    if (procedureStepItems.Any())
                    {
                        var returnedProcedureStepData = ValidateProcedureStepData(procedureStepItems);
                        if (returnedProcedureStepData.Any())
                        {
                            var importError = new ImportError()
                            {
                                Errors = new List<string>() { $"Procedure with Id: {procedureImportItem.Id} and Name: {procedureImportItem.Name} has {returnedProcedureStepData.Count()} Step Errors." },
                                Line = lineNumber
                            };
                            importError.Errors.AddRange(returnedProcedureStepData.SelectMany(i => i.Errors.Select(j => $"Line: {i.Line}: Error: {j}")));
                            errors.Add(importError);
                        }

                        foreach (var procedureStepItem in procedureStepItems)
                        {
                            if (!string.IsNullOrWhiteSpace(procedureStepItem.RoleIds))
                            {
                                var roleIds = procedureStepItem.RoleIds.Split(", ");
                                foreach (var roleIdStr in roleIds)
                                {
                                    if (Int32.TryParse(roleIdStr, out var roleId))
                                    {
                                        if (allRoleIds.Contains(roleId))
                                        {
                                            procedureStepItem.Roles.Add(new Domain.Models.Role()
                                            {
                                                Id = roleId
                                            });
                                        }
                                    }
                                }
                            }
                        }

                        
                        procedureImportItem.ProcedureSteps = procedureStepItems;
                    }
                    if (!errors.Any())
                    {
                        validImportItems.Add(procedureImportItem);
                    }
                }
                catch (InvalidCastException e)
                {
                    List<string> errorStrings = new List<string>
                    {
                        $"Procedure with Id: {procedureImportItem.Id} and Name: {procedureImportItem.Name} has a Parse Error: {e.Message}"
                    };
                    errors.Add(new ImportError()
                    {
                        Errors = errorStrings,
                        Line = lineNumber
                    });
                }
                lineNumber += 1;
            }
            return (errors, validImportItems);
        }

        private List<ImportError> ValidateProcedureStepData(List<ProcedureStepImportItem> procedureSteps)
        {
            int lineNumber = 1;
            var errors = new List<ImportError>();
            const int DEFAULTROLE = 7; //Technician

            var roleIds = _unitOfWork.Roles.Query().Select(i => i.Id).ToList();
            var procedureStepIds = _unitOfWork.ProcedureSteps.Query().Select(i => i.Id).ToList();

            foreach (var step in procedureSteps)
            {
                try
                {
                    if (!step.Id.HasValue)
                    {
                        errors.Add(new ImportError()
                        {
                            Errors = new List<string>()
                            {
                                $"Step on line: {lineNumber} does not have an Id"
                            },
                            Line = lineNumber
                        });
                    } else if (step.Id.HasValue && step.Id.Value > 0 && !procedureStepIds.Contains(step.Id.Value))
                    {
                        errors.Add(new ImportError()
                        {
                            Errors = new List<string>()
                            {
                                $"Step on line: {lineNumber} with Id: {step.Id.Value} Does not exist"
                            },
                            Line = lineNumber
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(step.RoleIds))
                    {
                        var inComingRoleIds = step.RoleIds.Split(", ");
                        foreach (var roleIdStr in inComingRoleIds)
                        {
                            if (Int32.TryParse(roleIdStr, out var roleId))
                            {
                                if (!roleIds.Contains(roleId))
                                {
                                    errors.Add(new ImportError()
                                    {
                                        Errors = new List<string>()
                                        {
                                                $"Step on line: {lineNumber} with Role Id: {roleId} Does not exist" 
                                        },
                                        Line = lineNumber
                                    });
                                }
                            }
                        }
                    }
                }
                catch (InvalidCastException e)
                {
                    List<string> errorStrings = new List<string>();
                    errorStrings.Add($"Step on Line: {lineNumber} has a Parse Error: {e.Message}");
                    errors.Add(new ImportError()
                    {
                        Errors = errorStrings,
                        Line = lineNumber
                    });
                }
                lineNumber += 1;
            }
            return errors;
        }

        private async Task<ICollection<Domain.Models.Procedure>> GetSingleProcedureAsync(GetProcedure command)
        {
            if (!command.Id.HasValue)
            {
                throw new DomainException("ID must have value", DomainError.BadRequest);
            }

            List<EntityFramework.Entities.Procedure> procedures;
            procedures = await _unitOfWork.Procedures
                .Query()
                .Where(x => x.Id == command.Id.Value)
                .Include(x => x.ProcedureType)
                .ToListAsync();
            if (procedures.Count == 0)
            {
                throw new DomainException($"procedure ID {command.Id.Value} not found", DomainError.NotFound);
            }

            // map and attach the right files for this object, if any
            var result = procedures.Select(x =>
            {
                var procedureModel = _mapper.Map<Domain.Models.Procedure>(x);
                procedureModel.ReferenceFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.Procedure), procedureModel.Id).ToList();

                return procedureModel;
            }).OrderBy(x => x.Name).ToList();

            return result;

        }

        private async Task<ICollection<Domain.Models.Procedure>> GetAllProceduresAsync()
        {
            ICollection<ProcedureWithUsedProductCountView> procedures =
                await _unitOfWork.Query<EntityFramework.Entities.Procedure>()
                    .GetProceduresWithProductCount(
                        ProcedureProjections.ProcedureWithUsedProductCountView);

            var result = _mapper.Map<ICollection<Domain.Models.Procedure>>(procedures);

            return result;
        }

        private async Task DeleteProcedureStepMonitorsAsync(int procedureStepId)
        {
            var procedureStepMonitorIdsToDelete = await _unitOfWork.ProcedureStepMonitors.Query()
                .Where(s => s.ProcedureStepId.Value == procedureStepId).Select(m => m.Id).ToListAsync();

            var workOrderTaskMonitorEntities = await _unitOfWork.WorkOrderTaskMonitors.Query()
                .Where(s => procedureStepMonitorIdsToDelete.Contains(s.ProcedureMonitorId.Value)).ToListAsync();

            workOrderTaskMonitorEntities.ForEach(workOrderTaskMonitorEntity =>
            {
                workOrderTaskMonitorEntity.ProcedureMonitorId = null;
            });

            var procedureStepMonitorEntities = await _unitOfWork.ProcedureStepMonitors.Query()
                .Where(s => s.ProcedureStepId == procedureStepId).ToListAsync();

            procedureStepMonitorEntities.ForEach(procedureStepMonitorEntity =>
            {
                procedureStepMonitorEntity.ProcedureStepId = null;
                _unitOfWork.ProcedureStepMonitors.Delete(false, procedureStepMonitorEntity);
            });

        }

        private async Task DeleteProcedureStepsAsync(int procedureStepId)
        {
            var workOrderTaskEntities =
                await _unitOfWork.WorkOrderTasks.Query().Where(s => s.ProcedureStepId == procedureStepId).ToListAsync();

            workOrderTaskEntities.ForEach(workOrderTaskEntity =>
            {
                workOrderTaskEntity.ProcedureStepId = null;
            });

            var procedureStepEntity = await _unitOfWork.ProcedureSteps.FirstOrDefaultAsync(false, s => s.Id == procedureStepId);

            procedureStepEntity.ProcedureId = null;

            var productStepEntities = await _unitOfWork.ProductSteps.Query()
                .Where(s => s.ProcedureStepId == procedureStepId).ToListAsync();

            productStepEntities.ForEach(productStepEntity =>
            {
                productStepEntity.ProcedureStepId = null;
            });

            _unitOfWork.ProcedureSteps.Delete(false, procedureStepEntity);

        }
    }
}
