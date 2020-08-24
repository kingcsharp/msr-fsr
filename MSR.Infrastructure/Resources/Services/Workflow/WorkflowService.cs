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

            var workflowStagesMap = await _unitOfWork.WorkflowStageMaps.Query()
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

        public async Task<PendingApprovalNotification> GetApprovalNotificationsAsync(GetPendingApprovalsModel command)
        {
            var pendingNotificationItems = new List<PendingNotificationItem>() { };

            if (CurrentUser.CanReadActivity(EnumApprovalTables.CustomerApproval))
            {
                var pendingNotificationCount = await _unitOfWork.CustomerApprovals.CountAsync(i => i.StatusId == (int)ApprovalStatusEnum.InProgress || i.StatusId == (int)ApprovalStatusEnum.Pending);
                if (pendingNotificationCount > 0)
                {
                    pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Customers", Table = (int)EnumApprovalTables.CustomerApproval, Count = pendingNotificationCount });
                }
            }
            if (CurrentUser.CanReadActivity(EnumApprovalTables.LocationApproval))
            {
                var locationApprovalCount = await _unitOfWork.LocationApprovals.CountAsync();
                if (locationApprovalCount > 0)
                {
                    pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Locations", Table = (int)EnumApprovalTables.LocationApproval, Count = locationApprovalCount });
                }
            }
            if (CurrentUser.CanReadActivity(EnumApprovalTables.PartApproval))
            {
                var partsApprovalCount = await _unitOfWork.PartApprovals.CountAsync(i => i.StatusId == (int)ApprovalStatusEnum.InProgress || i.StatusId == (int)ApprovalStatusEnum.Pending);
                if (partsApprovalCount > 0)
                {
                    pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Parts", Table = (int)EnumApprovalTables.PartApproval, Count = partsApprovalCount });
                }
            }
            if (CurrentUser.CanReadActivity(EnumApprovalTables.ProcedureApproval))
            {
                var procedureApprovalCount = await _unitOfWork.ProcedureApprovals.CountAsync(i => i.StatusId == (int)ApprovalStatusEnum.InProgress || i.StatusId == (int)ApprovalStatusEnum.Pending);
                if (procedureApprovalCount > 0)
                {
                    pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Procedures", Table = (int)EnumApprovalTables.ProcedureApproval, Count = procedureApprovalCount });
                }

            }
            if (CurrentUser.CanReadActivity(EnumApprovalTables.PurchaseOrderApproval))
            {
                var purchaseOrderApprovalCount = await _unitOfWork.PurchaseOrderApprovals.CountAsync(i => i.StatusId == (int)ApprovalStatusEnum.InProgress || i.StatusId == (int)ApprovalStatusEnum.Pending);
                if (purchaseOrderApprovalCount > 0)
                {
                    pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Purchase Orders", Table = (int)EnumApprovalTables.PurchaseOrderApproval, Count = purchaseOrderApprovalCount });
                }

            }
            if (CurrentUser.CanReadActivity(EnumApprovalTables.UserApproval))
            {
                var userApprovalCount = await _unitOfWork.UserApprovals.CountAsync(i => i.StatusId == (int)ApprovalStatusEnum.InProgress || i.StatusId == (int)ApprovalStatusEnum.Pending);
                if (userApprovalCount > 0)
                {
                    pendingNotificationItems.Add(new PendingNotificationItem() { Name = "Users", Table = (int)EnumApprovalTables.UserApproval, Count = userApprovalCount });
                }
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

            var result = await workflow.Include(x => x.MemberStages)
                .Include(x => x.ActivityMaps).ToListAsync();

            var memberStagesIds = result.Select(x => x.MemberStages.Select(u => u.WorkflowStageId))
                .SelectMany(workflowIds => workflowIds).Distinct().ToList();

            var interactivityIds = result.Select(x => x.ActivityMaps.Select(u => u.WorkflowActivityId))
                .SelectMany(activityIds => activityIds).Distinct().ToList();

            var workflowActivityMaps = _unitOfWork.WorkflowActivityMaps.Query()
                .Where(x => interactivityIds.Contains(x.WorkflowActivityId))
                .Select(x => x.WorkflowActivity)
                .ToList();

            var workflowStageMaps = _unitOfWork.WorkflowStageMaps.Query()
                .Where(x => memberStagesIds.Contains(x.WorkflowStageId))
                .Select(x => x.WorkflowStage)
                 .ToList();

            var ret = result.Select(workflowGrou => _mapper.Map<WorkflowModel>(workflowGrou)).ToList();
            return ret;
        }

        public async Task<WorkflowModel> CreateWorkFlowAsync(CreateWorkflowModel command)
        {
            var efWorkflow = _mapper.Map<EntityFramework.Entities.Workflow>(command);

            await _unitOfWork.Workflows.AddAndSaveChangesAsync(efWorkflow);

            var workflow = await this.GetWorkFlowAsync(new GetWorkflowModel() { Id = efWorkflow.Id });

            return workflow.FirstOrDefault();
        }

        public async Task<WorkflowModel> UpdateWorkFlowAsync(UpdateWorkflowModel command)
        {
            var efWorkFlow = await _unitOfWork.Workflows.Query().Include(x => x.MemberStages)
                .Include(x => x.ActivityMaps).FirstOrDefaultAsync(x => x.Id == command.Id);
            foreach (var item in efWorkFlow.MemberStages)
            {
                _unitOfWork.WorkflowStageMaps.Delete(false, item, true);
            }

            foreach (var item in efWorkFlow.ActivityMaps)
            {
                _unitOfWork.WorkflowActivityMaps.Delete(false, item, true);
            }

            await _unitOfWork.SaveChangesAsync();

            efWorkFlow.Name = command.Name;
            efWorkFlow.IsActive = command.IsActive;

            efWorkFlow.MemberStages = command.MemberStages.Select(
                x => _unitOfWork.WorkflowStageMaps
                .AttachAndInsert(_mapper.Map<WorkflowStageMap>(x)))
                .ToList();

            efWorkFlow.ActivityMaps = command.ActivityMaps.Select(
                x => _unitOfWork.WorkflowActivityMaps
                .AttachAndInsert(_mapper.Map<WorkflowActivityMap>(x)))
                .ToList();

            _unitOfWork.Workflows.Update(efWorkFlow);
            await _unitOfWork.SaveChangesAsync();

            var workflow = await this.GetWorkFlowAsync(new GetWorkflowModel() { Id = efWorkFlow.Id });

            return workflow.FirstOrDefault();
        }

        public async Task DeactivateWorkFlowAsync(DeactivateWorkflowModel command)
        {
            var workFlow = await _unitOfWork.Workflows.Query()
                .Include(x => x.ActivityMaps)
                .Include(x => x.MemberStages)
                .FirstOrDefaultAsync(x => x.Id == command.Id);
            if (workFlow == null)
            {
                return;
            }
            _unitOfWork.Workflows.Delete(false, workFlow, true);
            await _unitOfWork.SaveChangesAsync();
        }




    }
}
