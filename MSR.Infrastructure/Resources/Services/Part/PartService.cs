using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using MSR.Infrastructure.Resources.Queries;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Part
{
    public class PartService : IPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IMessageHubClient _messageHub;

        public PartService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IMessageHubClient messageHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _messageHub = messageHub;
        }

        public async Task<ICollection<PartModel>> GetPartsAsync(GetParts command)
        {
            List<EntityFramework.Entities.Part> partEntities = await _unitOfWork.Parts.Query().CreatePartQuery(command).ToListAsync();

            if (command.Id.HasValue && partEntities.Count == 0)
            {
                throw new DomainException($"part ID {command.Id.Value} not found", DomainError.NotFound);
            }

            var result = partEntities.Select(x => _mapper.Map<PartModel>(x)).ToList();

            return result;
        }

        public async Task<PartModel> CreatePartAsync(CreatePart command)
        {
            PartModel ret;

            if (CurrentUser.HasPrivilege(EnumMenuItem.Parts, EnumPrivilege.CanApprove))
            {
                List<PartSubPartMap> children = new List<PartSubPartMap>();
                if (command.SubParts != null)
                {
                    children = command.SubParts.Select(x =>
                        _mapper.Map<PartSubPartMap>(x)
                    ).ToList();
                }

                EntityFramework.Entities.Part part =
                    _mapper.Map<EntityFramework.Entities.Part>(command);
                if (children.Count > 0)
                {
                    part.IsKit = true;
                }
                _unitOfWork.Parts.Add(part);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.LogApprovalTransaction(part, part.Id, "Approved", command.Comment);

                if (children.Count > 0)
                {
                    foreach (var child in children)
                    {
                        child.ParentPartId = part.Id;
                        _unitOfWork.PartSubPartMaps.Add(child);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                ret = _mapper.Map<PartModel>(part);
            }
            else
            {
                // The row in the Part table is not created until approval,
                // so submit all the data to the approval.
                var approval = _mapper.Map<PartApproval>(command);
                approval.ApprovalJSON = JsonConvert.SerializeObject(command);
                approval.PartId = null;
                approval.WorkflowId = GetWorkflowID();
                approval.WorkflowGroupId = GetWorkflowGroupID(approval.WorkflowId);
                _unitOfWork.PartApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                ret = new PartModel()
                {
                    IsPending = true
                };
                _messageHub.SendApprovalNotification(EnumApprovalTables.PartApproval);
            }

            return ret;
        }
        public async Task<PartModel> UpdatePartAsync(UpdatePart command)
        {
            EntityFramework.Entities.Part current = await _unitOfWork.Parts.Query()
                .Include(x => x.Subparts)
                .Where(x => x.Id == command.Id)
                .FirstOrDefaultAsync();

            if (current is null)
            {
                throw new DomainException($"{nameof(Part)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            PartModel ret;

            if (CurrentUser.HasPrivilege(EnumMenuItem.Parts, EnumPrivilege.CanApprove))
            {
                foreach (var child in current.Subparts)
                {
                    _unitOfWork.PartSubPartMaps.Delete(false, child, true);
                }

                await _unitOfWork.SaveChangesAsync();
                current.Subparts.Clear();

                _mapper.Map(command, current);

                if (command.SubParts == null)
                {
                    current.Subparts = new List<PartSubPartMap>();
                }
                else if(current.IsKit == true)
                {
                    current.Subparts = command.SubParts.Select(x =>
                    {
                        x.ParentId = current.Id;
                        x.Id = null;
                        var subpart = _mapper.Map<PartSubPartMap>(x);
                        _unitOfWork.PartSubPartMaps.AttachAndInsert(subpart);
                        return subpart;
                    }).ToList();
                } else
                {
                    current.Subparts = new List<PartSubPartMap>();
                }

                current.IsKit = command.IsKit;
                _unitOfWork.Parts.Update(current);
                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(current, current.Id, "Approved", command.Comment);
                ret = _mapper.Map<PartModel>(current);
            }
            else
            {
                var approval = _mapper.Map<PartApproval>(command);
                approval.ApprovalJSON = JsonConvert.SerializeObject(command);
                approval.WorkflowId = GetWorkflowID();
                approval.WorkflowGroupId = GetWorkflowGroupID(approval.WorkflowId);

                _unitOfWork.PartApprovals.Add(approval);
                await _unitOfWork.SaveChangesAsync();

                current.IsKit = command.SubParts.Count > 0;
                ret = _mapper.Map<PartModel>(current);
                ret.IsPending = true;
                _messageHub.SendApprovalNotification(EnumApprovalTables.PartApproval);
            }

            return ret;
        }
        public async Task<PartModel> DeletePartAsync(DeletePart command)
        {
            EntityFramework.Entities.Part current = await _unitOfWork.Parts.Query().Include(x => x.Subparts).Include(x => x.WorkOrderParts).Where(x => x.Id == command.Id)
                .FirstOrDefaultAsync();
            if (current is null)
            {
                throw new DomainException($"{nameof(Part)} not found with ID: {command.Id}", DomainError.NotFound);
            }
            if (CurrentUser.HasPrivilege(EnumMenuItem.Parts, EnumPrivilege.CanDelete))
            {
                var subPartIds = current.Subparts.Select(x => x.Id).ToList();

                _unitOfWork.PartSubPartMaps
                    .Query()
                    .Where(x => subPartIds.Contains(x.Id))
                    .ToList()
                    .ForEach(x => {
                        _unitOfWork.PartSubPartMaps.Delete(false, x, true);
                    });

                var workOrderPartIds = current.WorkOrderParts.Select(x => x.Id).ToList();

                _unitOfWork.WorkOrderParts
                    .Query()
                    .Include(x => x.Children)
                    .Where(x => workOrderPartIds.Contains(x.Id))
                    .ToList()
                    .ForEach(x => {
                        foreach (var child in x.Children)
                        {
                            _unitOfWork.WorkOrderParts.Delete(false, child, true);
                        }
                        
                        _unitOfWork.WorkOrderParts.Delete(false, x, true);
                    });

                _unitOfWork.Parts.Delete(false, current, true);
                // This will call SaveChangesAsync
                await _unitOfWork.LogApprovalTransaction(current, current.Id);
            }
            else
            {
                throw new DomainException($"Permission denied for DELETE on {nameof(Part)} ID: {command.Id}", DomainError.BadRequest);
            }

            var partModel = _mapper.Map<PartModel>(current);
            return partModel;
        }

        private int GetWorkflowID()
        {
            var wfid = _unitOfWork.WorkflowActivityMaps
                .Query()
                .Where(x =>
                    x.WorkflowActivity.ApprovalTableName.Equals("PartApproval"))
                .Select(x => x.Workflow.Id);

            if (wfid.Any())
            {
                return wfid.First();
            }

            return -1;
        }

        private int GetWorkflowGroupID(int workflowID)
        {
            var wfsid = _unitOfWork.WorkflowStageMaps
                .Query()
                .Where(x => x.Workflow.Id == workflowID);

            if (!wfsid.Any())
            {
                return -1;
            }

            var wfgid = _unitOfWork.WorkflowGroupStageMaps
                .Query()
                .Where(x => x.WorkflowStageId == wfsid.First().WorkflowStageId);

            if (wfgid.Any())
            {
                return wfgid.First().WorkflowGroupId;
            }

            return -1;

        }

        public async Task<ICollection<PartModel>> ImportParts(byte[] excelData)
        {
            IEnumerable records = CSVHelper.ParseRecords<PartImportItem>("");
            var parsedRecords = XLSHelper.ParseRecords(excelData);
            List<UpdatePart> updates = new List<UpdatePart>();
            List<CreatePart> inserts = new List<CreatePart>();
            List<PartModel> results = new List<PartModel>();

            var partIds = await _unitOfWork.Parts.Query().Select(i => i.Id).ToListAsync();

            if (!CurrentUser.HasPrivilege(EnumMenuItem.Parts, EnumPrivilege.CanApprove))
            {
                throw new DomainException($"Permission denied for user {CurrentUser.GetId()}", DomainError.BadRequest);
            }

            parsedRecords.Tables[0].Rows.Cast<DataRow>().ToList().ForEach(r =>
            {

            EnumSegregationType segregationType;
            Enum.TryParse(r["SegregationType"]?.ToString(), out segregationType);

            var part = new PartImportItem
            {
                Name = r["Name"]?.ToString(),
                PartNumber = r["PartNumber"]?.ToString(),
                OEMPartNumber = r["OEMPartNumber"]?.ToString(),
                IsKit = Convert.ToBoolean(r["IsKit"]),
                IsActive = Convert.ToBoolean(r["IsActive"]),
                NickName = r["NickName"]?.ToString(),
            };

            if (r["Id"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["Id"].ToString()))
            {
                part.Id = Convert.ToInt32(r["Id"]);
            }


            if (Enum.IsDefined(typeof(EnumSegregationType), segregationType))
            {
                part.SegregationType = segregationType;
            }

            if (r["MaximumCycles"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["MaximumCycles"].ToString()))
            {
                part.MaximumCycles = Convert.ToInt32(r["MaximumCycles"]);
            }

            if (part.Id.HasValue && part.Id.Value > 0 && partIds.Contains(part.Id.Value))
            {
            var updatePartModel = _mapper.Map<UpdatePart>(part);
            updates.Add(updatePartModel);
            }
            else
                {
                var createPartModel = _mapper.Map<CreatePart>(part);
                inserts.Add(createPartModel);
            }
            });

            // Then perform the update
            foreach (UpdatePart model in updates)
            {
                results.Add(await UpdatePartAsync(model));
            }
            foreach (CreatePart model in inserts)
            {
                results.Add(await CreatePartAsync(model));
            }

            return results;
        }

        public async Task<int> GetTotalPartRows(GetParts command)
        {

            var totalRows = await _unitOfWork.Parts.Query().CreatePartQuery(command, true).CountAsync();

            return totalRows;

        }
    }
}
