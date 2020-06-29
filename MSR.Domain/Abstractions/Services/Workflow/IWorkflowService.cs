using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using MSR.Domain.Commands.Workflow;

namespace MSR.Domain.Abstractions.Services.Workflow
{
    public interface IWorkflowService
    {
        Task<PendingApprovalNotification> GetApprovalNotificationsAsync(GetPendingApprovals command);
    }
}
