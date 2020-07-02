using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Domain.Models.Workflow;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var result = await workflowStage.Include(x => x.Created)
                .Include(x => x.LastUpdated).ToListAsync();
            var ret = result.Select(workflowGrou => _mapper.Map<WorkflowStageModel>(workflowGrou)).ToList();
            return ret;
        }

        public async Task<WorkflowStageModel> CreateWorkFlowStageAsync(CreateWorkflowStageModel command)
        {
            var efWorkflowStage = _mapper.Map<WorkflowStage>(command);

            await _unitOfWork.WorkflowStages.AddAndSaveChangesAsync(efWorkflowStage);

            await _unitOfWork.SaveChangesAsync();

            var workFlowModel = _mapper.Map<WorkflowStageModel>(efWorkflowStage);

            return workFlowModel;
        }

        public async Task<WorkflowStageModel> UpdateWorkFlowStageAsync(UpdateWorkflowStageModel command)
        {
            var efWorkFlow = await _unitOfWork.WorkflowStages.Query().FirstOrDefaultAsync(x => x.Id == command.Id);

            if (efWorkFlow == null) {
                throw new DomainException(
                    $"{nameof(_unitOfWork.WorkflowStages)} {command.Id} not found",
                    Domain.Commanding.Enums.DomainError.NotFound
                );
            }

            efWorkFlow.Name = command.Name;
            efWorkFlow.IsActive = command.IsActive;


            _unitOfWork.WorkflowStages.Update(efWorkFlow);
            await _unitOfWork.SaveChangesAsync();

            var workFlowModel = _mapper.Map<WorkflowStageModel>(efWorkFlow);

            return workFlowModel;
        }

        public async Task DeactivateWorkFlowStageAsync(DeactivateWorkflowStage command)
        {
            var workFlow = await _unitOfWork.WorkflowStages.Query().FirstOrDefaultAsync(x => x.Id == command.Id);
            if (workFlow == null)
            {
                return;
            }
            _unitOfWork.WorkflowStages.Delete(false, workFlow, true);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
