using System.Collections.Generic;
using MSR.Domain.Commands;
using System.Threading.Tasks;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Models.Workflow;

namespace MSR.Domain.Abstractions.Services.Workflow
{
    public interface IWorkflowStageService
    {
        Task<ICollection<WorkflowStageModel>> GetWorkFlowStageAsync(GetWorkflowStageModel command);
        Task<WorkflowStageModel> CreateWorkFlowStageAsync(CreateWorkflowStageModel command);
        Task<WorkflowStageModel> UpdateWorkFlowStageAsync(UpdateWorkflowStageModel command);
        Task DeactivateWorkFlowStageAsync(DeactivateWorkflowStage command);
    }
}
