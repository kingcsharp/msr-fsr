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

namespace MSR.Infrastructure.Resources.Services.Workflow
{
    public class WorkflowGroupService : IWorkflowGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkflowGroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<WorkflowGroupModel>> GetWorkFlowGroupsAsync(GetWorkflowGroupsModel command)
        {
            var workflowGroups = _unitOfWork.WorkflowGroups.Query();

            if (command.Id.HasValue)
            {
                workflowGroups = workflowGroups.Where(i => i.Id == command.Id.Value);
            }

            var result = await workflowGroups.ToListAsync();
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

            efWorkflowGroup.GroupUsers = command.Users.Select(
                x => _unitOfWork.WorkflowGroupUserMaps
                .AttachAndInsert(_mapper.Map<WorkflowGroupUserMap>(x)))
                .ToList();

            await _unitOfWork.SaveChangesAsync();

            var workFlowModel = _mapper.Map<WorkflowGroupModel>(efWorkflowGroup);

            workFlowModel.GroupRoles = efWorkflowGroup.GroupRoles.Select(
                x => _mapper.Map<WorkflowGroupRoleMapModel>(x))
                .ToList();

            workFlowModel.GroupUsers = efWorkflowGroup.GroupUsers.Select(
                x => _mapper.Map<WorkflowGroupUserMapModel>(x))
                .ToList();

            return workFlowModel;
        }
        public async Task<WorkflowGroupModel> UpdateWorkFlowGroupAsync(UpdateWorkflowGroupModel command)
        {
            var efWorkFlow = await _unitOfWork.WorkflowGroups.Query().FirstOrDefaultAsync(x => x.Id == command.Id);

            foreach (var role in efWorkFlow.GroupRoles)
            {
                _unitOfWork.WorkflowGroupRoleMaps.Delete(false, role, true);
            }

            foreach (var role in efWorkFlow.GroupUsers)
            {
                _unitOfWork.WorkflowGroupUserMaps.Delete(false, role, true);
            }

            efWorkFlow.Name = command.Name;
            efWorkFlow.IsActive = command.IsActive;

            efWorkFlow.GroupRoles = command.Roles.Select(
                x => _unitOfWork.WorkflowGroupRoleMaps
                .AttachAndInsert(_mapper.Map<WorkflowGroupRoleMap>(x)))
                .ToList();

            efWorkFlow.GroupUsers = command.Users.Select(
                x => _unitOfWork.WorkflowGroupUserMaps
                .AttachAndInsert(_mapper.Map<WorkflowGroupUserMap>(x)))
                .ToList();

            _unitOfWork.WorkflowGroups.Update(efWorkFlow);
            await _unitOfWork.SaveChangesAsync();

            var workFlowModel = _mapper.Map<WorkflowGroupModel>(efWorkFlow);

            return workFlowModel;
        }

        public async Task DeactivateWorkFlowGroupAsync(DeactivateWorkflowGroup command)
        {
            var workFlow = await _unitOfWork.WorkflowGroups.Query()
                .Include(x => x.GroupRoles)
                .Include(x => x.GroupUsers)
                .FirstOrDefaultAsync(x => x.Id == command.Id);
            if (workFlow == null)
            {
                return;
            }
            _unitOfWork.WorkflowGroups.Delete(false, workFlow, true);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
