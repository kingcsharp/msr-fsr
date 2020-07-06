using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
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

            var result = await workflowGroups.Include(x => x.Created)
                .Include(x => x.LastUpdated).Include(x => x.GroupRoles).ToListAsync();
            var ret = result.Select(workflowGrou => _mapper.Map<WorkflowGroupModel>(workflowGrou)).ToList();
            return ret;
        }

        public async Task<WorkflowGroupModel> CreateWorkFlowGroupAsync(CreateWorkflowGroup command)
        {
            var efWorkflowGroup = _mapper.Map<WorkflowGroup>(command);

            try {
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
            } catch (DbUpdateException e) {
                throw new DomainException(
                    e.InnerException.Message,
                    Domain.Commanding.Enums.DomainError.NotFound
                );
            }
        }

        public async Task<WorkflowGroupModel> UpdateWorkFlowGroupAsync(UpdateWorkflowGroupModel command)
        {
            var efWorkFlow = await _unitOfWork.WorkflowGroups.Query()
                .Include(x => x.GroupRoles).FirstOrDefaultAsync(x => x.Id == command.Id);

            if (efWorkFlow == null) {
                throw new DomainException(
                    $"{nameof(_unitOfWork.WorkflowGroups)} {command.Id} not found",
                    DomainError.NotFound
                );
            }

            foreach (var role in efWorkFlow.GroupRoles)
            {
                _unitOfWork.WorkflowGroupRoleMaps.Delete(false, role);
            }

            efWorkFlow.Name = command.Name;
            efWorkFlow.IsActive = command.IsActive;

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
            var workFlow = await _unitOfWork.WorkflowGroups.Query().Include(x => x.GroupRoles).FirstOrDefaultAsync(x => x.Id == command.Id);
            if (workFlow == null)
            {
                return;
            }
            _unitOfWork.WorkflowGroups.Delete(false, workFlow, true);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
