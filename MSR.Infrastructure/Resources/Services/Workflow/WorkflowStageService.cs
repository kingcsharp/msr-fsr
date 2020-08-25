using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSR.Domain.Commanding.Enums;

namespace MSR.Infrastructure.Resources.Services.Workflow
{
    public class WorkflowStageService : IWorkflowStageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkflowStageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<WorkflowStageModel>> GetWorkFlowStageAsync(GetWorkflowStageModel command)
        {
            var workflowStage = _unitOfWork.WorkflowStages.Query();

            if (command.Id.HasValue)
            {
                workflowStage = workflowStage.Where(i => i.Id == command.Id.Value);
            }

            var result = await workflowStage.Include(x => x.Group).ToListAsync();
            var ret = result.Select(workflowGrou => _mapper.Map<WorkflowStageModel>(workflowGrou)).ToList();
            return ret;
        }

        public async Task<WorkflowStageModel> CreateWorkFlowStageAsync(CreateWorkflowStageModel command)
        {
            var efWorkflowStage = _mapper.Map<WorkflowStage>(command);

            await _unitOfWork.WorkflowStages.AddAndSaveChangesAsync(efWorkflowStage);
            var workFlowModel = _mapper.Map<WorkflowStageModel>(efWorkflowStage);

            return workFlowModel;
        }

        public async Task<WorkflowStageModel> UpdateWorkFlowStageAsync(UpdateWorkflowStageModel command)
        {
            var efWorkFlow = await _unitOfWork.WorkflowStages.Query().Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == command.Id);

            if (efWorkFlow == null)
            {
                throw new DomainException(
                    $"{nameof(_unitOfWork.WorkflowStages)} {command.Id} not found",
                    Domain.Commanding.Enums.DomainError.NotFound
                );
            }

            efWorkFlow.Name = command.Name;
            efWorkFlow.IsActive = command.IsActive;

            foreach (var item in efWorkFlow.Group)
            {
                _unitOfWork.WorkflowGroupStageMaps.Delete(false, item, true);
            }

            efWorkFlow.Group = command.WorkflowGroupStageMapModel.Select(
                x => _unitOfWork.WorkflowGroupStageMaps
                .AttachAndInsert(_mapper.Map<WorkflowGroupStageMap>(x)))
                .ToList();

            _unitOfWork.WorkflowStages.Update(efWorkFlow);
            await _unitOfWork.SaveChangesAsync();

            var workFlowModel = _mapper.Map<WorkflowStageModel>(efWorkFlow);

            return workFlowModel;
        }

        public async Task DeactivateWorkFlowStageAsync(DeactivateWorkflowStage command)
        {
            var workFlowStage = await _unitOfWork.WorkflowStages.Query().Include(x => x.Group)
                .FirstOrDefaultAsync(x => x.Id == command.Id);
            var cannotBeDeleted = _unitOfWork.WorkflowStageMaps.Query().Any(x => x.WorkflowStageId == command.Id);
            if (cannotBeDeleted)
            {
                throw new DomainException($"Can not remove {workFlowStage.Name} because it belongs to approval workflows.", DomainError.NotFound);
            }
            
            if (workFlowStage == null)
            {
                return;
            }

            foreach (var item in workFlowStage.Group)
            {
                _unitOfWork.WorkflowGroupStageMaps.Delete(false, item, true);
            }
            await _unitOfWork.SaveChangesAsync();

            _unitOfWork.WorkflowStages.Delete(false, workFlowStage, true);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
