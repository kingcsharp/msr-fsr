using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MSR.Domain.Abstractions.Services.Workflow
{
    public interface IWorkflowService
    {
        Task<PendingApprovalNotification> GetApprovalNotificationsAsync(GetPendingApprovals command);
        Task<ICollection<WorkflowActivityModel>> GetWorkFlowAsync(GetWorkflowActivities command);
        Task<ICollection<WorkflowModel>> GetWorkFlowAsync(GetWorkflowModel command);
        Task<WorkflowModel> CreateWorkFlowAsync(CreateWorkflowModel command);
        Task<WorkflowModel> UpdateWorkFlowAsync(UpdateWorkflowModel command);
        Task DeactivateWorkFlowAsync(DeactivateWorkflowModel command);
        
    }
}
