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

        public async Task<ICollection<PendingApprovalModel>> GetPendingApprovalAsync(GetPendingApproval command)
        {
            IQueryable<ApprovalEntity> approvalEntity = null;
            switch (command.Table)
            {
                case EnumApprovalTables.CustomerApproval:
                    approvalEntity = _unitOfWork.CustomerApprovals.Query();
                    break;
                case EnumApprovalTables.DocumentApproval:
                    approvalEntity = _unitOfWork.DocumentApprovals.Query();
                    break;
                case EnumApprovalTables.LocationApproval:
                    approvalEntity = _unitOfWork.LocationApprovals.Query();
                    break;
                case EnumApprovalTables.PartApproval:
                    approvalEntity = _unitOfWork.PartApprovals.Query();
                    break;
                case EnumApprovalTables.ProcedureApproval:
                    approvalEntity = _unitOfWork.ProcedureApprovals.Query();
                    break;
                case EnumApprovalTables.ProductApproval:
                    approvalEntity = _unitOfWork.ProductApprovals.Query();
                    break;
                case EnumApprovalTables.PurchaseOrderApproval:
                    approvalEntity = _unitOfWork.PurchaseOrderApprovals.Query();
                    break;
                case EnumApprovalTables.UserApproval:
                    approvalEntity = _unitOfWork.UserApprovals.Query();
                    break;
                default:
                    break;
            }

            var approvalEntityList = await approvalEntity.ToListAsync();
            var ret = approvalEntityList.Select(approvalEnt => _mapper.Map<PendingApprovalModel>(approvalEnt)).ToList();
            return ret;
        }

        public async Task<PendingApprovalNotification> GetApprovalNotificationsAsync(GetPendingApprovals command)
        {
            //TODO: Make this less brittle
            var inProcressStatusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == "In Progress").Id;
            var pendingStatusId = _unitOfWork.Status.FirstOrDefault(false, i => i.Name == "Pending").Id;

            return new PendingApprovalNotification()
            {
                Customers = _unitOfWork.CustomerApprovals.Count(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId),
                Locations = _unitOfWork.LocationApprovals.Count(),
                Parts = _unitOfWork.PartApprovals.Count(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId),
                Procedures = _unitOfWork.ProcedureApprovals.Count(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId),
                PurchaseOrders = _unitOfWork.PurchaseOrderApprovals.Count(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId),
                Users = _unitOfWork.UserApprovals.Count(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId)
            };
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
