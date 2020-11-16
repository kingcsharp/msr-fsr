using AutoMapper.Mappers;
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
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Part
{
    public class ProcedureService : IProcedureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ProcedureValidator _validator;

        public ProcedureService(IUnitOfWork unitOfWork,
            IMapper mapper, IFileService fileService, ProcedureValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _validator = validator; // used for import
        }

        public async Task<ICollection<Domain.Models.Procedure>> GetProcedureAsync(GetProcedure command)
        {
            List<EntityFramework.Entities.Procedure> procedures;
            if (command.procedureID.HasValue)
            {
                procedures = await _unitOfWork.Procedures
                    .Query()
                    .Where(x => x.Id == command.procedureID.Value)
                    .Include(x => x.ProcedureType)
                    .Include(x => x.ReferenceFiles)
                    .ToListAsync();
                if (procedures.Count == 0)
                {
                    throw new DomainException($"procedure ID {command.procedureID.Value} not found", DomainError.NotFound);
                }
            }
            else
            {
                procedures = await _unitOfWork.Procedures
                    .Query()
                    .Include(x => x.ProcedureType)
                    .Include(x => x.ReferenceFiles)
                    .ToListAsync();
            }

            // map and attach the right files for this object, if any
            var result = procedures.Select(x =>
            {
                var model = _mapper.Map<Domain.Models.Procedure>(x);
                model.ReferenceFiles = new List<Domain.Models.FileModel>();
                foreach (FileEntityMap map in x.ReferenceFiles)
                {
                    if (map.EntityTableName != nameof(EntityFramework.Entities.Procedure))
                    {
                        continue;
                    }
                    model.ReferenceFiles.Add(
                        _mapper.Map<Domain.Models.FileModel>(map.FileObject)
                    );
                }
                return model;
            }).OrderBy(x => x.Name).ToList();

            return result;
        }
        public async Task<Domain.Models.Procedure> CreateProcedureAsync(CreateProcedure command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Procedure ret;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval))
            {
                var procedure = _mapper.Map<EntityFramework.Entities.Procedure>(command);
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                var created = _unitOfWork.Procedures.Add(procedure);
                await _unitOfWork.SaveChangesAsync();

                // The API return expects the procedure type object to be loaded.
                created.Context.Entry(procedure).Reference(x => x.ProcedureType).Load();

                ret = _mapper.Map<Domain.Models.Procedure>(procedure);
            }
            else
            {
                var approval = _mapper.Map<ProcedureApproval>(command);
                approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
                approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
                approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int) ApprovalStatusEnum.Pending);
                _unitOfWork.ProcedureApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.Procedure>(approval);
            }

            return ret;
        }
        public async Task<Domain.Models.Procedure> UpdateProcedureAsync(UpdateProcedure command)
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
            Domain.Models.Procedure ret;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval))
            {
                var procedure = _mapper.Map(command, current);
                _unitOfWork.Procedures.Update(procedure);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                ret = _mapper.Map<Domain.Models.Procedure>(procedure);
            }
            else
            {
                var approval = _mapper.Map<ProcedureApproval>(command);
                approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
                approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
                approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int) ApprovalStatusEnum.Pending);
                _unitOfWork.ProcedureApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.Procedure>(approval);
            }

            return ret;

        }
        public async Task<ICollection<Domain.Models.ProcedureStepModel>> GetProcedureStepAsync(GetProcedureStep command)
        {
            List<EntityFramework.Entities.ProcedureStep> steps;
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
            var result = steps.Select(x => _mapper.Map<Domain.Models.ProcedureStepModel>(x)).OrderBy(x => x.PrintOrder).ToList();

            var procedureStepIds = result.Select(m => m.Id).ToList();
            var documentEntityMaps = await _unitOfWork.DocumentEntityMap.Query().Where(s =>
                procedureStepIds.Contains(s.EntityId) &&
                s.EntityTableName == nameof(EntityFramework.Entities.ProcedureStep)).ToListAsync();

            result.ForEach(procedureStep =>
            {
                procedureStep.ReferenceFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.ProcedureStep), procedureStep.Id).ToList();
                procedureStep.ReferenceDocumentIds = documentEntityMaps.Where(s => s.EntityId == procedureStep.Id).Select(m => m.DocumentId).ToList();
            });

            return result;
        }

        public async Task<Domain.Models.ProcedureStepModel> CreateProcedureStepAsync(CreateProcedureStep command)
        {

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval))
            {
                var procedureStepEntity = _mapper.Map<ProcedureStep>(command);
                await _unitOfWork.ProcedureSteps.AddAsync(procedureStepEntity);
                await _unitOfWork.LogApprovalTransaction(procedureStepEntity, procedureStepEntity.Id);

                return _mapper.Map<Domain.Models.ProcedureStepModel>(procedureStepEntity);
            }

            var currentProcedure = await _unitOfWork.Procedures
                .Query()
                .FirstAsync(x => x.Id == command.procedureId);

            int procApprovalId = await FlagProcedureForApproval(currentProcedure, command.procedureId);

            var procedureStepApprovalEntity = _mapper.Map<ProcedureStepApproval>(command);
            procedureStepApprovalEntity.ProcedureApprovalId = procApprovalId;
            await _unitOfWork.ProcedureStepApprovals.AddAsync(procedureStepApprovalEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Domain.Models.ProcedureStepModel>(procedureStepApprovalEntity);
        }

        public async Task<Domain.Models.ProcedureStepModel> UpdateProcedureStepAsync(UpdateProcedureStep command)
        {
            ProcedureStep current = await _unitOfWork.ProcedureSteps
                .Query()
                .Include(x => x.StepType)
                .Include(x => x.ReferenceFiles)
                .FirstOrDefaultAsync(i =>
                    i.Id == command.procedureStepId && i.ProcedureId == command.procedureId);

            if (current is null)
            {
                throw new DomainException($"{nameof(ProcedureStep)} not " +
                    $"found with ID: {command.procedureId} / {command.procedureStepId}",
                    DomainError.NotFound);
            }

            _unitOfWork.ProcedureSteps.LoadCollection(current, "ProcedureStepRoles");

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.ProcedureStepModel ret;

            // Upload any new files, but do not attach yet.
            if (command.ReferenceFiles != null &&
                command.ReferenceFiles.Count > 0)
            {
                if (command.ReferenceFileIds == null)
                {
                    command.ReferenceFileIds = new List<int>();
                }
                foreach (FileModel file in command.ReferenceFiles)
                {
                    FileModel newFile = await _fileService.CreateFileAsync(
                        nameof(EntityFramework.Entities.ProcedureStep),
                        0, // will be attached after checking perms
                        file
                    );
                    command.ReferenceFileIds.Add(newFile.FileId.Value);
                }
            }

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval))
            {
                if (command.Roles != null &&
                    command.Roles.Count > 0 &&
                    command.Roles.All(x => x != null))
                {
                    foreach (ProcedureStepRoleMap m in current.ProcedureStepRoles)
                    {
                        _unitOfWork.ProcedureStepRoleMaps.Delete(false, m);
                    }
                }

                // update EF object with update command data
                var step = _mapper.Map(command, current);

                // If we're updating the file list, detach and re-attach
                if (command.ReferenceFileIds.Count > 0)
                {
                    await _fileService.DetachFilesAsync(
                        nameof(EntityFramework.Entities.ProcedureStep),
                        current.Id);

                    foreach (int fileId in command.ReferenceFileIds)
                    {
                        await _fileService.MapUploadedFileAsync(
                            nameof(EntityFramework.Entities.ProcedureStep),
                            current.Id, fileId
                        );
                    }
                }

                if (command.ReferenceDocumentIds != null &&
                    command.ReferenceDocumentIds.Count > 0)
                {
                    List<int> currentIds = await _unitOfWork.DocumentEntityMap
                        .Query()
                        .Where(x => x.EntityId == current.Id &&
                            x.EntityTableName.Equals(nameof(EntityFramework.Entities.ProcedureStep)))
                        .Select(x => x.Id)
                        .ToListAsync();
                    foreach (int id in currentIds)
                    {
                        _unitOfWork.DocumentEntityMap.Delete(false, id);
                    }
                    foreach (int newDocId in command.ReferenceDocumentIds)
                    {
                        var ndem = new DocumentEntityMap()
                        {
                            EntityId = current.Id,
                            EntityTableName = nameof(EntityFramework.Entities.ProcedureStep),
                            DocumentId = newDocId
                        };
                        await _unitOfWork.DocumentEntityMap.AddAsync(ndem);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                if (step.ProcedureStepRoles != null)
                {
                    foreach (ProcedureStepRoleMap m in step.ProcedureStepRoles)
                    {
                        m.ProcedureStepId = step.Id;
                    }
                }
                _unitOfWork.ProcedureSteps.Update(step);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(step, step.Id);

                ret = _mapper.Map<Domain.Models.ProcedureStepModel>(step);
            }
            else
            {
                _unitOfWork.ProcedureSteps.LoadReference(current, x => x.Procedure);
                int procApprovalId = await FlagProcedureForApproval(
                    current.Procedure, current.ProcedureId
                );
                var approval = _mapper.Map<ProcedureStepApproval>(command);
                approval.ProcedureApprovalId = procApprovalId;

                string json = JsonConvert.SerializeObject(new
                {
                    roleIds = command.Roles.Select(x => x.Id).ToList(),
                        fileIds = command.ReferenceFileIds,
                        documentIds = command.ReferenceDocumentIds,
                });
                approval.ApprovalJSON = json;

                _unitOfWork.ProcedureStepApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = _mapper.Map<Domain.Models.ProcedureStepModel>(approval);
            }

            return ret;
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
                _unitOfWork.Procedures.Delete(false, current);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.Procedure)} uid {CurrentUser.GetId()}");
            }

            return true;
        }

        public async Task<ProcedureStepModel> DeleteProcedureStepAsync(DeleteProcedureStep command)
        {
            var current = await _unitOfWork.ProcedureSteps.FirstOrDefaultAsync(false,
                i => i.ProcedureId == command.procedureID && i.Id == command.procedureStepID
            );

            current.ProcedureStepRoles = await _unitOfWork.ProcedureStepRoleMaps.Query()
                .Where(s => s.ProcedureStepId == command.procedureStepID).ToListAsync();

            current.ProcedureStepMonitors = await _unitOfWork.ProcedureStepMonitors.Query()
                .Where(s => s.ProcedureStepId == command.procedureStepID).ToListAsync();

            if (current is null)
            {
                throw new DomainException($"{nameof(ProcedureStep)} not found with ID: {command.procedureID}/{command.procedureStepID}", DomainError.NotFound);
            }
            if (CurrentUser.HasPrivilege(EnumMenuItem.RunnableProcedures, EnumPrivilege.CanDelete))
            {
                _unitOfWork.ProcedureSteps.Delete(false, current);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new DomainException($"Permission deined for {nameof(Domain.Models.ProcedureStepModel)} uid {CurrentUser.GetId()}");
            }

            var ProcedureStepModel = _mapper.Map<ProcedureStepModel>(current);

            return ProcedureStepModel;
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
                approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int) ApprovalStatusEnum.Pending);

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

            ParsedProcedureImport import =
                _validator.ValidateAndReturnImportData(xlsData, out importErrors);

            // Iterate through each procedure and add it, adding
            // each procedure's step along the way.
            foreach (string procid in import.procedures.Keys)
            {
                CreateProcedure newProc = import.procedures[procid];

                // set defaults not included in import
                if (newProc.DurationType == null)
                {
                    newProc.DurationType = "hours";
                }

                Domain.Models.Procedure createdProcedure =
                    await CreateProcedureAsync(newProc);

                if (import.procedureSteps.ContainsKey(procid))
                {
                    List<CreateProcedureStep> steps = import.procedureSteps[procid];
                    foreach (var step in steps)
                    {
                        step.procedureId = createdProcedure.Id;
                        ProcedureStepModel newStep =
                            await CreateProcedureStepAsync(step);
                        // TODO: there's no place in the procedure model to store
                        // the new procedure step ids.
                    }
                }

                createdProcs.Add(createdProcedure);
            }

            return createdProcs;
        }
    }
}
