using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services.Workflow
{
    public interface IWorkflowService
    {
        Task<PendingApprovalNotification> GetApprovalNotificationsAsync(GetPendingApprovals command);
    }
}
