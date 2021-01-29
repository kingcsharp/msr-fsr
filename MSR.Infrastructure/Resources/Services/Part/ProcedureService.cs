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
using System.Data;
using System;
using System.Diagnostics;
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
        public Dictionary<string, CreateProcedure> procedures;
        public Dictionary<string, CreateProcedureImport> procedureExtras;
        public Dictionary<string, List<CreateProcedureStep>> procedureSteps;
        public Dictionary<string, List<CreateProcedureStepImport>> procedureStepExtras;
    }

    public class ProcedureService : IProcedureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ProcedureValidator _validator;

        const int IMPORT_TABLE_COUNT = 2;
        const string PROC_STEP_EXPORT = "PROC_STEP_EXPORT";
        const string PROCEDURE_NAME_EXPORT = "PROCEDURE_NAME_EXPORT";
        const int PROCEDURESTEPS_COLUMNS_COUNT = 13;
        const int PROCEDURES_COLUMNS_COUNT = 4;


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
            if (command.procedureID.HasValue) {
                return await GetSingleProcedureAsync(command);
            } else {
                return await GetAllProceduresAsync();
            }
        }

        public async Task<Domain.Models.Procedure> CreateProcedureAsync(CreateProcedure command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Procedure ret;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval))
            {
                var procedure = _mapper.Map<EntityFramework.Entities.Procedure>(command);
                procedure.Revision = 1;
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
            Domain.Models.Procedure procedureModel;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval))
            {
                int revision = current.Revision;
                var procedure = _mapper.Map(command, current);
                procedure.Revision = revision + 1;
                _unitOfWork.Procedures.Update(procedure);

                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(procedure, procedure.Id);

                procedureModel = _mapper.Map<Domain.Models.Procedure>(procedure);

                // Update Reference Files mapping
                var currentFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.Procedure), command.Id);
                var currentFileIds = currentFiles.Select(i => i.FileId).ToList();
                var fileIdsToAdd = command.ReferenceFileIds.Where(i => !currentFileIds.Contains(i)).ToList();
                var fileIdsToRemove = currentFileIds.Where(i => !command.ReferenceFileIds.Contains(i.Value)).ToList();

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

                 foreach (var file in command.ReferenceFiles)
                {
                    var fileModel = await _fileService.CreateFileAsync(nameof(EntityFramework.Entities.Procedure), procedureModel.Id, file);

                    fileReferences.Add(fileModel);
                }

                procedureModel.ReferenceFiles = fileReferences;
            }
            else
            {
                var approval = _mapper.Map<ProcedureApproval>(command);
                approval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(approval);
                approval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(approval.Workflow?.Id ?? 0);
                approval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int) ApprovalStatusEnum.Pending);
                _unitOfWork.ProcedureApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                procedureModel = _mapper.Map<Domain.Models.Procedure>(approval);
            }

            return procedureModel;

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
            var procedureStepModels = steps.Select(x => _mapper.Map<Domain.Models.ProcedureStepModel>(x)).OrderBy(x => x.PrintOrder).ToList();

            var procedureStepIds = procedureStepModels.Select(m => m.Id).ToList();
            var documentEntityMaps = await _unitOfWork.DocumentEntityMap.Query().Where(s =>
                procedureStepIds.Contains(s.EntityId) &&
                s.EntityTableName == nameof(EntityFramework.Entities.ProcedureStep)).ToListAsync();

            var workOrderTasksInUse = _unitOfWork.WorkOrderTasks.Query()
                .Where(s => procedureStepIds.Contains(s.ProcedureStepId.Value)
                            && (s.StatusId == (int)EnumStatusSteps.InProgress 
                               || s.StatusId == (int)EnumStatusSteps.WaitingtoStart
                               || s.StatusId == (int)EnumStatusSteps.Approved));

            procedureStepModels.ForEach(procedureStep =>
            {
                procedureStep.ReferenceFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.ProcedureStep), procedureStep.Id).ToList();
                procedureStep.ReferenceDocumentIds = documentEntityMaps.Where(s => s.EntityId == procedureStep.Id).Select(m => m.DocumentId).ToList();
                procedureStep.IsUsed = workOrderTasksInUse.Any(s => s.ProcedureStepId == procedureStep.Id);
            });

            return procedureStepModels;
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

        public async Task<Domain.Models.ProcedureStepModel> UpdateProcedureStepAsync(UpdateProcedureStep command, bool incRevision = true)
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

            foreach (var commandReferenceFileId in command.ReferenceFileIds)
            {
                idsOfFilesToBeMappedAndSaved.Add(commandReferenceFileId);
            }

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.ProcedureApproval))
            {
                if (command.Roles != null &&
                    command.Roles.Count > 0 &&
                    command.Roles.All(x => x != null))
                {
                    foreach (ProcedureStepRoleMap m in originalProcedureStepEntity.ProcedureStepRoles)
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
                    await _unitOfWork.SaveChangesAsync();
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

                return _mapper.Map<ProcedureStepModel>(updatedProcedureStepEntity);
            }
            else
            {
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

                return _mapper.Map<ProcedureStepModel>(approval);
            }

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
                    .ForEach(id => {
                        _unitOfWork.ProcedureStepRoleMaps.Delete(false, id);
                    });

                _unitOfWork.ProcedureStepMonitors
                    .Query()
                    .Where(x => procedureStepIds.Contains(x.ProcedureStepId.Value))
                    .Select(x => x.Id)
                    .ToList()
                    .ForEach(id => {
                        _unitOfWork.ProcedureStepMonitors.Delete(false, id);
                    });

                _unitOfWork.CascadeDelete(current);
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

            ParsedProcedureImport import = ParseData(xlsData, out importErrors);
            if (importErrors.Any() || import == null)
            {
                string allErrors = string.Join("\n", importErrors.Select(x => {
                    string errMsg = string.Join(", ", x.Errors);
                    return $"Line {x.Line}: {errMsg}";
                }));
                throw new DomainException(allErrors, DomainError.BadRequest);
            }

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

        private ParsedProcedureImport ParseData(byte[] binData, out IEnumerable<ImportError> importErrors)
        {
            var errors = new List<ImportError>();
            List<string> errorStrings = new List<string>();
            var result = XLSHelper.ParseRecords(binData);

            DataTableCollection tables = result.Tables;

            if (tables.Count != IMPORT_TABLE_COUNT)
            {
                errorStrings.Add($"Invalid table count {tables.Count} != 2");
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            DataTable procedureSteps = tables[0];
            DataTable procedures = tables[1];

            if (!procedureSteps.TableName.ToUpper().Equals(PROC_STEP_EXPORT))
            {
                errorStrings.Add($"Invalid table name: {procedureSteps.TableName} != {PROC_STEP_EXPORT}");
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            if (!procedures.TableName.ToUpper().Equals(PROCEDURE_NAME_EXPORT))
            {
                errorStrings.Add($"Invalid table name: {procedureSteps.TableName} != {PROCEDURE_NAME_EXPORT}");
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            if (procedureSteps.Columns.Count != PROCEDURESTEPS_COLUMNS_COUNT)
            {
                errorStrings.Add(
                    $"Invalid {PROC_STEP_EXPORT} row count " +
                    $"{procedureSteps.Columns.Count} != " +
                    PROCEDURESTEPS_COLUMNS_COUNT.ToString()
                );
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            if (procedures.Columns.Count != PROCEDURES_COLUMNS_COUNT)
            {
                errorStrings.Add(
                    $"Invalid {PROC_STEP_EXPORT} row " +
                    $"count {procedures.Columns.Count} != " +
                    PROCEDURES_COLUMNS_COUNT.ToString()
                );
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            ParsedProcedureImport parsedData = new ParsedProcedureImport();
            //
            // procedure import
            //
            errors = ImportProcedureData(procedures, parsedData);

            //
            // procedure step import
            //
            errors.AddRange(ImportProcedureStepData(procedureSteps, parsedData));

            importErrors = errors;
            return parsedData;
        }

        private List<ImportError> ImportProcedureData(DataTable procedures,
            ParsedProcedureImport parsedData)
        {
            bool isHeader = true;
            Dictionary<string, int> fieldMap = new Dictionary<string, int>();
            int lineNumber = 1;
            var errors = new List<ImportError>();

            Dictionary<string, CreateProcedure> newProcs =
                new Dictionary<string, CreateProcedure>();
            Dictionary<string, CreateProcedureImport> newProcsExtra =
                new Dictionary<string, CreateProcedureImport>();
            foreach (DataRow proc in procedures.Rows)
            {
                string procedureIdString = "";
                try
                {
                    if (isHeader)
                    {
                        int i = 0;
                        foreach (string item in proc.ItemArray)
                        {
                            fieldMap.Add(item, i);
                            i += 1;
                        }
                        isHeader = false;
                        lineNumber += 1;
                        continue;
                    }

                    CreateProcedure newProc = new CreateProcedure();
                    CreateProcedureImport newProcExtra =
                        new CreateProcedureImport();
                    procedureIdString = proc.ItemArray[fieldMap["PROCEDURE_ID"]].ToString();
                    newProc.Name = proc.ItemArray[fieldMap["PROCEDURE_NAME"]].ToString();
                    newProcExtra.ANS_ID = proc.ItemArray[fieldMap["ANS_ID"]].ToString();
                    newProc.ProcedureTypeId = Convert.ToInt32((double) proc.ItemArray[fieldMap["PROC_TYPE_ID"]]);

                    // Store the new procedure data in memory.
                    if (newProcs.ContainsKey(procedureIdString))
                    {
                        List<string> errorStrings = new List<string>();
                        errorStrings.Add($"Import ERROR: duplicate key '{procedureIdString}'");
                        errors.Add(new ImportError() { Errors = errorStrings });
                    }
                    newProcs.Add(procedureIdString, newProc);
                    newProcsExtra.Add(procedureIdString, newProcExtra);
                }
                catch (InvalidCastException e)
                {
                    List<string> errorStrings = new List<string>();
                    errorStrings.Add($"PROC({procedureIdString}) Parse Error: {e.Message}");
                    errors.Add(new ImportError()
                    {
                        Errors = errorStrings,
                            Line = lineNumber
                    });
                }
                lineNumber += 1;
            }
            parsedData.procedures = newProcs;
            parsedData.procedureExtras = newProcsExtra;
            return errors;
        }

        private List<ImportError> ImportProcedureStepData(DataTable procedureSteps,
            ParsedProcedureImport parsedData)
        {
            bool isHeader = true;
            Dictionary<string, int> fieldMap = new Dictionary<string, int>();
            int lineNumber = 1;
            var errors = new List<ImportError>();

            Dictionary<string, List<CreateProcedureStep>> newSteps =
                new Dictionary<string, List<CreateProcedureStep>>();
            Dictionary<string, List<CreateProcedureStepImport>> newStepsExtra =
                new Dictionary<string, List<CreateProcedureStepImport>>();
            foreach (DataRow step in procedureSteps.Rows)
            {
                string procedureIdString = "";
                try
                {
                    if (isHeader)
                    {
                        int i = 0;
                        foreach (string item in step.ItemArray)
                        {
                            fieldMap.Add(item, i);
                            i += 1;
                        }
                        isHeader = false;
                        lineNumber += 1;
                        continue;
                    }

                    CreateProcedureStep newStep = new CreateProcedureStep();
                    CreateProcedureStepImport newStepExtra =
                        new CreateProcedureStepImport();

                    procedureIdString = step.ItemArray[fieldMap["PROCEDURE_ID"]].ToString();
                    newStep.StepText = step.ItemArray[fieldMap["STEP_TEXT"]].ToString();
                    newStep.PrintOrder = Convert.ToInt32((double) step.ItemArray[fieldMap["PRINT_ORDER"]]);
                    newStepExtra.COMMENT = step.ItemArray[fieldMap["COMMENT"]].ToString();
                    newStep.LaborTime = Convert.ToInt32(((double) step.ItemArray[fieldMap["STEP_TIME"]]));
                    newStepExtra.EXTRA_NOTE1 = step.ItemArray[fieldMap["EXTRA_NOTE1"]].ToString();
                    newStep.Roles = ((double) step.ItemArray[fieldMap["DEFAULT_ROLE_ID"]]).ToString();
                    newStepExtra.SERIALIZE = step.ItemArray[fieldMap["SERIALIZE"]].ToString();
                    newStepExtra.SUCCESS_MONITOR = step.ItemArray[fieldMap["SUCCESS_MONITOR"]].ToString();
                    newStepExtra.INTERNAL_LOCATION = step.ItemArray[fieldMap["INTERNAL_LOCATION"]].ToString();
                    newStepExtra.LOC_TYPE = step.ItemArray[fieldMap["LOC_TYPE"]].ToString();
                    newStep.Title = step.ItemArray[fieldMap["Title"]].ToString();

                    // Reference documents are stored by string.  The IDs will need
                    // to be fetched from database later
                    newStepExtra.REF_DOC_ID = step.ItemArray[fieldMap["REF_DOC_ID"]].ToString();

                    // The IDs used in the spreadsheet will need to be collated at creation
                    // time, so for now we just store a separate mapping.
                    List<CreateProcedureStep> psmImport;
                    if (newSteps.ContainsKey(procedureIdString))
                    {
                        psmImport = newSteps[procedureIdString];
                    }
                    else
                    {
                        psmImport = new List<CreateProcedureStep>();
                        newSteps.Add(procedureIdString, psmImport);
                    }
                    psmImport.Add(newStep);

                    List<CreateProcedureStepImport> psmiImport;
                    if (newStepsExtra.ContainsKey(procedureIdString))
                    {
                        psmiImport = newStepsExtra[procedureIdString];
                    }
                    else
                    {
                        psmiImport = new List<CreateProcedureStepImport>();
                        newStepsExtra.Add(procedureIdString, psmiImport);
                    }
                    psmiImport.Add(newStepExtra);
                }
                catch (InvalidCastException e)
                {
                    List<string> errorStrings = new List<string>();
                    errorStrings.Add($"STEP({procedureIdString}) Parse Error: {e.Message}");
                    errors.Add(new ImportError()
                    {
                        Errors = errorStrings,
                            Line = lineNumber
                    });
                }
                lineNumber += 1;
            }
            parsedData.procedureSteps = newSteps;
            parsedData.procedureStepExtras = newStepsExtra;
            return errors;
        }

        private async Task<ICollection<Domain.Models.Procedure>> GetSingleProcedureAsync(GetProcedure command)
        {
            if (!command.procedureID.HasValue)
            {
                throw new DomainException("ID must have value", DomainError.BadRequest);
            }

            List<EntityFramework.Entities.Procedure> procedures;
            procedures = await _unitOfWork.Procedures
                .Query()
                .Where(x => x.Id == command.procedureID.Value)
                .Include(x => x.ProcedureType)
                .ToListAsync();
            if (procedures.Count == 0)
            {
                throw new DomainException($"procedure ID {command.procedureID.Value} not found", DomainError.NotFound);
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
            var workOrderTaskMonitorEntities =  await _unitOfWork.WorkOrderTaskMonitors.Query()
                .Where(s => s.ProcedureMonitorId == procedureStepId).ToListAsync();

            workOrderTaskMonitorEntities.ForEach(workOrderTaskMonitorEntity =>
            {
                workOrderTaskMonitorEntity.ProcedureMonitorId = null;
            });

            var procedureStepMonitorEntities = await _unitOfWork.ProcedureStepMonitors.Query()
                .Where(s => s.ProcedureStepId == procedureStepId).ToListAsync();

            procedureStepMonitorEntities.ForEach(procedureStepMonitorEntity =>
            {
                procedureStepMonitorEntity.ProcedureStepId = null;
            });

            procedureStepMonitorEntities.ForEach( procedureStepMonitorEntity =>
            {
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
