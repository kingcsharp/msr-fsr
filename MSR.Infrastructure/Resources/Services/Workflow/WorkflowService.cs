using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Models;
using MSR.Domain.Models.Workflow;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                Users = _unitOfWork.UserApprovals.Count(i => i.StatusId == inProcressStatusId || i.StatusId == pendingStatusId),
            };
        }

        public async Task<ICollection<WorkflowModel>> GetWorkFlowAsync(GetWorkflowModel command)
        {
            var workflow = _unitOfWork.Workflows.Query();

            if (command.Id.HasValue)
            {
                workflow = workflow.Where(i => i.Id == command.Id.Value);
            }

            var result = await workflow.Include(x => x.Created)
                .Include(x => x.LastUpdated).Include(x => x.ActivityMaps).Include(x => x.MemberStages).ToListAsync();
            var ret = result.Select(workflowGrou => _mapper.Map<WorkflowModel>(workflowGrou)).ToList();
            return ret;
        }

        public async Task<WorkflowModel> CreateWorkFlowAsync(CreateWorkflowModel command)
        {
            var efWorkflow = _mapper.Map<EntityFramework.Entities.Workflow>(command);

            await _unitOfWork.Workflows.AddAndSaveChangesAsync(efWorkflow);

            efWorkflow.MemberStages = command.MemberStages.Select(
                x => _unitOfWork.WorkflowStagesMap
                .AttachAndInsert(_mapper.Map<WorkflowStageMap>(x)))
                .ToList();

            efWorkflow.ActivityMaps = command.ActivityMaps.Select(
                x => _unitOfWork.WorkflowActivityMaps
                .AttachAndInsert(_mapper.Map<WorkflowActivityMap>(x)))
                .ToList();

            await _unitOfWork.SaveChangesAsync();

            var workFlowModel = _mapper.Map<WorkflowModel>(efWorkflow);

            workFlowModel.MemberStages = efWorkflow.MemberStages.Select(
                x => _mapper.Map<WorkflowStageMapModel>(x))
                .ToList();

            workFlowModel.ActivityMaps = efWorkflow.ActivityMaps.Select(
                x => _mapper.Map<WorkflowActivityMapModel>(x))
                .ToList();

            return workFlowModel;
        }

        public async Task<WorkflowModel> UpdateWorkFlowAsync(UpdateWorkflowModel command)
        {
            var efWorkFlow = await _unitOfWork.Workflows.Query()
                .Include(x => x.ActivityMaps)
                .Include(x => x.MemberStages).FirstOrDefaultAsync(x => x.Id == command.Id);

            foreach (var role in efWorkFlow.MemberStages)
            {
                _unitOfWork.WorkflowStagesMap.Delete(false, role);
            }

            foreach (var role in efWorkFlow.ActivityMaps)
            {
                _unitOfWork.WorkflowActivityMaps.Delete(false, role);
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
            var workFlow = await _unitOfWork.Workflows.Query().Include(x => x.ActivityMaps)
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
