using System.Collections.Generic;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Threading.Tasks;
using MSR.Domain.Commands.Workflow;

namespace MSR.Domain.Abstractions.Services.Workflow
{
    public interface IWorkflowGroupService
    {
        Task<ICollection<WorkflowGroupModel>> GetWorkFlowGroupsAsync(GetWorkflowGroupsModel command);
        Task<WorkflowGroupModel> CreateWorkFlowGroupAsync(CreateWorkflowGroupModel command);
        Task<WorkflowGroupModel> UpdateWorkFlowGroupAsync(UpdateWorkflowGroupModel command);
        Task DeactivateWorkFlowGroupAsync(DeactivateWorkflowGroup command);
    }
}
