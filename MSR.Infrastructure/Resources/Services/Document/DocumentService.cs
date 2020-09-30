using IronPdf;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

namespace MSR.Infrastructure.Resources.Services.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IUploadFiles _fileUploader;
        private readonly IDownloadFiles _fileDownloader;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IFileHandlerFactory fileHanderFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;

            _fileUploader = fileHanderFactory.CreateUploader(FileProvider.S3);
            _fileDownloader = fileHanderFactory.CreateDownloader(FileProvider.S3);
        }

        public async Task<ICollection<DocumentView>> GetDocuments(int? id)
        {
            var documentsQuery = _unitOfWork.Documents.Query()
                .Include(d => d.DocumentEntityMaps)
                .Include(d => d.Roles)
                .ThenInclude(r => r.Role)
                .AsQueryable();

            if (id.HasValue && id > 0)
            {
                documentsQuery = documentsQuery.Where(d => d.Id == id);
            }

            var documents = await documentsQuery.ToListAsync();

            var retDocumentViews = _mapper.Map<ICollection<DocumentView>>(documents);

            foreach (var dv in retDocumentViews)
            {
                dv.ReferenceFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.Document), dv.Id);

                dv.RoleIds = documents.Where(d => d.Id == dv.Id).FirstOrDefault().Roles.Select(r => r.RoleId).Cast<int>().ToList();
            }

            return retDocumentViews;
        }

        public async Task<DocumentView> CreateDocumentAsync(CreateDocument command)
        {
            if (!CurrentUser.CanApproveActivity(EnumApprovalTables.DocumentApproval))
            {
                var documentApproval = _mapper.Map<DocumentApproval>(command);
                documentApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(documentApproval);
                documentApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(documentApproval.Workflow?.Id ?? 0);
                documentApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                documentApproval.ApprovalJSON = JsonConvert.SerializeObject(new { command.RoleIds, command.ReferenceFileIds });

                await _unitOfWork.DocumentApprovals.AddAsync(documentApproval);

                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<DocumentView>(documentApproval);
            }

            var document = _mapper.Map<EntityFramework.Entities.Document>(command);

            document.Revision = 1;

            await _unitOfWork.Documents.AddAsync(document);
            await _unitOfWork.LogApprovalTransaction(document, document.Id);

            var retDocument = _mapper.Map<DocumentView>(document);

            var listDocumentRoles = new List<DocumentRoleMap>();

            foreach (var roleId in command.RoleIds)
            {
                var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == roleId);

                if (role is null)
                {
                    continue;
                }

                var documentRoles = new DocumentRoleMap()
                {
                    DocumentId = retDocument.Id,
                    RoleId = role.Id
                };

                await _unitOfWork.DocumentRoles.AddAsync(documentRoles);

                listDocumentRoles.Add(documentRoles);
            }

            await _unitOfWork.SaveChangesAsync();

            retDocument.RoleIds = command.RoleIds;

            var fileReferences = new List<FileModel>();

            foreach (var fileId in command.ReferenceFileIds)
            {
                var fileModel = await _fileService.MapUploadedFileAsync(nameof(EntityFramework.Entities.Document), retDocument.Id, fileId);

                fileReferences.Add(fileModel);
            }

            foreach (var file in command.ReferenceFiles)
            {
                var fileModel = await _fileService.CreateFileAsync(nameof(EntityFramework.Entities.Document), retDocument.Id, file);

                fileReferences.Add(fileModel);
            }

            retDocument.ReferenceFiles = fileReferences;
            retDocument.ReferenceFileIds = fileReferences.Select(f => f.FileId).Cast<int>().ToList();

            return retDocument;
        }

        public async Task<DocumentView> UpdateDocumentAsync(UpdateDocument command)
        {
            var currentDocument = await _unitOfWork.Documents.Query().Include(d => d.Roles).FirstOrDefaultAsync(i => i.Id == command.Id);

            if (currentDocument is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Document)} not found with ID: {command.Id}");
            }

            DocumentView retDocument;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.DocumentApproval))
            {
                currentDocument.Revision += 1;

                _mapper.Map(command, currentDocument);

                _unitOfWork.Documents.Update(currentDocument);
                await _unitOfWork.LogApprovalTransaction(currentDocument, currentDocument.Id);

                retDocument = _mapper.Map<DocumentView>(currentDocument);

                // Update Document roles mapping
                var currentRoles = currentDocument.Roles.Select(i => i.RoleId).ToList();
                var roleIdsToAdd = command.RoleIds.Where(i => !currentRoles.Contains(i)).ToList();
                var roleIdsToRemove = currentRoles.Where(i => !command.RoleIds.Contains(i)).ToList();

                var rolesToRemove = _unitOfWork.DocumentRoles.Query().Where(i => i.DocumentId == command.Id && roleIdsToRemove.Contains(i.RoleId)).ToList();

                foreach (var addRoleId in roleIdsToAdd)
                {
                    var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == addRoleId);

                    if (role is null) continue;

                    _unitOfWork.DocumentRoles.Add(new DocumentRoleMap()
                    {
                        DocumentId = currentDocument.Id,
                        RoleId = role.Id
                    });
                }

                foreach (var removeRole in rolesToRemove)
                {
                    _unitOfWork.DocumentRoles.Delete(false, removeRole);
                }

                await _unitOfWork.SaveChangesAsync();

                retDocument.RoleIds = currentRoles.Except(roleIdsToRemove).ToList();
                retDocument.RoleIds.AddRange(roleIdsToAdd);

                // Update Document files mapping
                var currentFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.Document), command.Id);

                var currentFileIds = currentFiles.Select(i => i.FileId).ToList();
                var fileIdsToAdd = command.ReferenceFileIds.Where(i => !currentFileIds.Contains(i)).ToList();
                var fileIdsToRemove = currentFileIds.Where(i => !command.ReferenceFileIds.Contains(i.Value)).ToList();

                var fileReferences = new List<FileModel>();

                foreach (var addFileId in fileIdsToAdd)
                {
                    var fileModel = await _fileService.MapUploadedFileAsync(nameof(EntityFramework.Entities.Document), command.Id, addFileId);

                    fileReferences.Add(fileModel);
                }

                foreach (var removeFileId in fileIdsToRemove)
                {
                    await _fileService.DetachFilesAsync(nameof(EntityFramework.Entities.Document), command.Id, removeFileId);
                }

                fileReferences.AddRange(_fileService.ListFiles(nameof(EntityFramework.Entities.Document), command.Id));

                foreach (var file in command.ReferenceFiles)
                {
                    var fileModel = await _fileService.CreateFileAsync(nameof(EntityFramework.Entities.Document), retDocument.Id, file);

                    fileReferences.Add(fileModel);
                }

                retDocument.ReferenceFiles = fileReferences;
                retDocument.ReferenceFileIds = fileReferences.Select(f => f.FileId).Cast<int>().ToList();
            }
            else
            {
                var documentApproval = _mapper.Map<DocumentApproval>(currentDocument);
                _mapper.Map(command, documentApproval);

                documentApproval.DocumentId = currentDocument.Id;
                documentApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(documentApproval);
                documentApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(documentApproval.Workflow?.Id ?? 0);
                documentApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
                documentApproval.ApprovalJSON = JsonConvert.SerializeObject(new { command.RoleIds, command.ReferenceFileIds });

                await _unitOfWork.DocumentApprovals.UpdateAndSaveChangesAsync(documentApproval);

                retDocument = _mapper.Map<DocumentView>(documentApproval);
            }

            return retDocument;
        }

        public async Task DeleteDocumentAsync(DeleteDocument command)
        {
            var document = await _unitOfWork.Documents.Query()
                                .Include(d => d.DocumentEntityMaps)
                                .Include(d => d.Roles)
                                .FirstOrDefaultAsync(i => i.Id == command.Id);

            var documentApproval = await _unitOfWork.DocumentApprovals.FirstOrDefaultAsync(false, i => i.DocumentId == command.Id);

            if (document is null && documentApproval is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Document)} not found", DomainError.BadRequest);
            }

            if (document != null)
            {
                foreach (var role in document.Roles)
                {
                    _unitOfWork.DocumentRoles.Delete(false, role);
                }

                foreach (var map in document.DocumentEntityMaps)
                {
                    _unitOfWork.DocumentEntityMap.Delete(false, map);
                }

                _unitOfWork.Documents.Delete(false, document);

            }

            if (documentApproval != null)
            {
                _unitOfWork.DocumentApprovals.Delete(false, documentApproval);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
