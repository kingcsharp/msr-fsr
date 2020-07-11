using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSR.Domain.Commanding.Enums;
using System;
using MSR.Domain.Helpers;

namespace MSR.Infrastructure.Resources.Services.Workflow
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkflowService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<WorkflowLinkModel>> GetAllMyActivitiesPrivileges(int? answerUserId)
        {
            var workflowMenuRoles = new List<WorkflowLinkModel>();
            if (answerUserId == null)
            {
                return workflowMenuRoles;
            }

            var workflowGroupIds = await _unitOfWork.WorkflowGroupUserMaps.Query()
                 .Where(y => y.UserId == answerUserId.Value)
                 .Select(x => x.WorkflowGroupId).Distinct()
                 .ToListAsync();

            var workflowGroups = await _unitOfWork.WorkflowGroups.Query()
                 .Select(x => new { x.Id, Roles = x.GroupRoles })
                 .Where(x => workflowGroupIds.Contains(x.Id))
                 .ToListAsync();

            var workflowStages = await _unitOfWork.WorkflowGroupStageMaps.Query()
                .Where(x => workflowGroupIds.Contains(x.WorkflowGroupId))
                .Select(x => new { x.WorkflowGroupId, x.WorkflowStageId }).ToListAsync();

            var stagesIds = workflowStages.Select(m => m.WorkflowStageId).ToList();

            var workflowStagesMap = await _unitOfWork.WorkflowStagesMap.Query()
                .Where(x => stagesIds.Contains(x.WorkflowStageId))
                .Select(x => new { x.WorkflowId, x.WorkflowStageId }).ToListAsync();

            var workflowIds = workflowStagesMap.Select(x => x.WorkflowId).ToList();

            var workflowActivities = await _unitOfWork.WorkflowActivityMaps.Query()
                .Where(x => workflowIds.Contains(x.WorkflowId))
                .Select(x => new { x.WorkflowActivityId, x.WorkflowActivity.ApprovalTableName, x.WorkflowActivity.MenuItemId, x.WorkflowId }).ToListAsync();


            foreach (var activity in workflowActivities)
            {
                var wfLink = new WorkflowLinkModel()
                {
                    WorkflowActivity = activity.WorkflowActivityId,
                    ApprovalTableName = activity.ApprovalTableName,
                    WorkflowId = activity.WorkflowId,
                    MenuItemId = activity.MenuItemId
                };
                foreach (var item in workflowStagesMap.Where(item => wfLink.WorkflowId == item.WorkflowId))
                {
                    wfLink.WorkflowStageId = item.WorkflowId;
                    foreach (var wfStage in workflowStages.Where(wfStage => wfStage.WorkflowStageId == wfLink.WorkflowStageId))
                    {
                        wfLink.WorkflowGroupId = wfStage.WorkflowGroupId;
                        foreach (var wfGroups in workflowGroups.Where(wfGroups => wfLink.WorkflowGroupId == wfGroups.Id))
                        {
                            wfLink.RoleIds = wfGroups.Roles.Select(x => x.RoleId).ToList();
                        }
                    }
                }
                workflowMenuRoles.Add(wfLink);
            }

            return workflowMenuRoles;
        }

        public async Task<PendingApprovalNotification> GetApprovalNotificationsAsync(GetPendingApprovals command)
        {
            var statuss = _unitOfWork.Status.Query().Where(i => i.Name == "In Progress" || i.Name == "Pending").ToList();
            var inProcressStatusId = statuss.FirstOrDefault(i => i.Name == "In Progress").Id;
            var pendingStatusId = statuss.FirstOrDefault(i => i.Name == "Pending").Id;

            var pendingNotificationItems = new List<PendingNotificationItem>() { };

            if (DelegateHandler.CanReadActivity(EnumApprovalTables.CustomerApproval))
            {
                pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Customers", Table = (int)EnumApprovalTables.CustomerApproval, Count = await _unitOfWork.CustomerApprovals.CountAsync(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId) });
            }
            if (DelegateHandler.CanReadActivity(EnumApprovalTables.LocationApproval))
            {
                pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Locations", Table = (int)EnumApprovalTables.LocationApproval, Count = await _unitOfWork.LocationApprovals.CountAsync() });
            }
            if (DelegateHandler.CanReadActivity(EnumApprovalTables.PartApproval))
            {
                pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Parts", Table = (int)EnumApprovalTables.PartApproval, Count = await _unitOfWork.PartApprovals.CountAsync(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId) });
            }
            if (DelegateHandler.CanReadActivity(EnumApprovalTables.ProcedureApproval))
            {
                pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Procedures", Table = (int)EnumApprovalTables.ProcedureApproval, Count = await _unitOfWork.ProcedureApprovals.CountAsync(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId) });
            }
            if (DelegateHandler.CanReadActivity(EnumApprovalTables.PurchaseOrderApproval))
            {
                pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Purchase Orders", Table = (int)EnumApprovalTables.PurchaseOrderApproval, Count = await _unitOfWork.PurchaseOrderApprovals.CountAsync(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId) });
            }
            if (DelegateHandler.CanReadActivity(EnumApprovalTables.UserApproval))
            {
                pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Users", Table = (int)EnumApprovalTables.UserApproval, Count = await _unitOfWork.UserApprovals.CountAsync(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId) });
            }

            var result = new PendingApprovalNotification()
            {
                Items = pendingNotificationItems
            };

            return result;
        }

        public async Task<ICollection<WorkflowActivityModel>> GetWorkFlowAsync(GetWorkflowActivities command)
        {
            var workflowActivities = await _unitOfWork.WorkflowActivities.Query().ToListAsync();

            var ret = workflowActivities.Select(wActivities => _mapper.Map<WorkflowActivityModel>(wActivities)).ToList();
            return ret;
        }

        public async Task<ICollection<WorkflowModel>> GetWorkFlowAsync(GetWorkflowModel command)
        {
            var workflow = _unitOfWork.Workflows.Query();

            if (command.Id.HasValue)
            {
                workflow = workflow.Where(i => i.Id == command.Id.Value);
            }

            var result = await workflow.ToListAsync();
            var ret = result.Select(workflowGrou => _mapper.Map<WorkflowModel>(workflowGrou)).ToList();
            return ret;
        }

        public async Task<WorkflowModel> CreateWorkFlowAsync(CreateWorkflowModel command)
        {
            var efWorkflow = _mapper.Map<EntityFramework.Entities.Workflow>(command);

            await _unitOfWork.Workflows.AddAndSaveChangesAsync(efWorkflow);

            var workFlowModel = _mapper.Map<WorkflowModel>(efWorkflow);

            return workFlowModel;
        }

        public async Task<WorkflowModel> UpdateWorkFlowAsync(UpdateWorkflowModel command)
        {
            var efWorkFlow = await _unitOfWork.Workflows.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
            foreach (var role in efWorkFlow.MemberStages)
            {
                _unitOfWork.WorkflowStagesMap.Delete(false, role, true);
            }

            foreach (var role in efWorkFlow.ActivityMaps)
            {
                _unitOfWork.WorkflowActivityMaps.Delete(false, role, true);
            }

            efWorkFlow.Name = command.Name;
            efWorkFlow.IsActive = command.IsActive;

            efWorkFlow.MemberStages = command.MemberStages.Select(
                x => _unitOfWork.WorkflowStagesMap
                .AttachAndInsert(_mapper.Map<WorkflowStageMap>(x)))
                .ToList();

            efWorkFlow.ActivityMaps = command.ActivityMaps.Select(
                x => _unitOfWork.WorkflowActivityMaps
                .AttachAndInsert(_mapper.Map<WorkflowActivityMap>(x)))
                .ToList();

            _unitOfWork.Workflows.Update(efWorkFlow);
            await _unitOfWork.SaveChangesAsync();

            var workFlowModel = _mapper.Map<WorkflowModel>(efWorkFlow);

            return workFlowModel;
        }

        public async Task DeactivateWorkFlowAsync(DeactivateWorkflowModel command)
        {
            var workFlow = await _unitOfWork.Workflows.Query()
                .Include(x => x.ActivityMaps)
                .Include(x => x.MemberStages).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (workFlow == null)
            {
                return;
            }
            _unitOfWork.Workflows.Delete(false, workFlow, true);
            await _unitOfWork.SaveChangesAsync();
        }




    }
}
