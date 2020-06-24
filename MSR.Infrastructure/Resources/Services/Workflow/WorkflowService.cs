using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
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

        public async Task<ICollection<WorkflowGroupModel>> GetWorkFlowGroupsAsync(GetWorkflowGroupsModel command)
        {
            var workflowGroups = _unitOfWork.WorkflowGroups.Query();

            if (command.Id.HasValue)
            {
                workflowGroups = workflowGroups.Where(i => i.Id == command.Id.Value);
            }

            var result = await workflowGroups.Include(x => x.GroupRoles).ToListAsync();
            var ret = result.Select(workflowGrou => _mapper.Map<WorkflowGroupModel>(workflowGrou)).ToList();
            return ret;
        }

        public async Task<WorkflowGroupModel> CreateWorkFlowGroupAsync(CreateWorkflowGroupModel command)
        {
            var efWorkflowGroup = _mapper.Map<WorkflowGroup>(command);

            await _unitOfWork.WorkflowGroups.AddAndSaveChangesAsync(efWorkflowGroup);
            efWorkflowGroup.GroupRoles = command.Roles.Select(
                x => _unitOfWork.WorkflowGroupRoleMaps
                .AttachAndInsert(_mapper.Map<WorkflowGroupRoleMap>(x)))
                .ToList();

            await _unitOfWork.SaveChangesAsync();

            var workFlowModel = _mapper.Map<WorkflowGroupModel>(efWorkflowGroup);

            workFlowModel.GroupRoles = efWorkflowGroup.GroupRoles.Select(
                x => _mapper.Map<WorkflowGroupRoleMapModel>(x))
                .ToList();

            return workFlowModel;
        }

        public async Task<WorkflowGroupModel> UpdateWorkFlowGroupAsync(UpdateWorkflowGroupModel command)
        {
            var efWorkFlow = await _unitOfWork.WorkflowGroups.Query()
                .Include(x => x.GroupRoles).FirstOrDefaultAsync(x => x.Id == command.Id);

            foreach (var role in efWorkFlow.GroupRoles)
            {
                _unitOfWork.WorkflowGroupRoleMaps.Delete(false, role);
            }

            efWorkFlow.Name = command.Name;

            efWorkFlow.GroupRoles = command.Roles.Select(
                x => _unitOfWork.WorkflowGroupRoleMaps
                .AttachAndInsert(_mapper.Map<WorkflowGroupRoleMap>(x)))
                .ToList();

            _unitOfWork.WorkflowGroups.Update(efWorkFlow);
            await _unitOfWork.SaveChangesAsync();

            var workFlowModel = _mapper.Map<WorkflowGroupModel>(efWorkFlow);

            return workFlowModel;
        }

        public async Task DeactivateWorkFlowGroupAsync(DeactivateWorkflow command)
        {
            var workFlow = await _unitOfWork.WorkflowGroups.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
            if (workFlow == null) { return; }
            workFlow.IsActive = !workFlow.IsActive;
            _unitOfWork.WorkflowGroups.Update(workFlow);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
