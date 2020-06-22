using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using MSR.Domain.Commands.Workflow;

namespace MSR.Domain.Abstractions.Services
{
    public interface IWorkflowService
    {
        Task<PendingApprovalNotification> GetApprovalNotificationsAsync(GetPendingApprovals command);
        Task<ICollection<WorkflowGroupModel>> GetWorkFlowGroupsAsync(GetWorkflowGroupsModel command);
        Task<WorkflowGroupModel> CreateWorkFlowGroupAsync(CreateWorkflowGroupModel command);
        Task<WorkflowGroupModel> UpdateWorkFlowGroupAsync(UpdateWorkflowGroupModel command);
        Task DeactivateWorkFlowGroupAsync(DeactivateWorkflow command);
    }
}
