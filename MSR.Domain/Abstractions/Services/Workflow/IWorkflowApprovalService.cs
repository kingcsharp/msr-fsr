
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IWorkflowApprovalService
    {
        Task<PendingApprovalModel> CreateApprovalAsync(PostApprovalModel command);
        Task<PendingApprovalModel> DeactivateApprovalAsync(DeactivateApprovalModel command);
        Task<ICollection<PendingApprovalModel>> GetPendingApprovalAsync(GetPendingApprovalModel command);
        Task<PendingApprovalPopoverModel> GetApprovalChangesAsync(GetPendingApprovalDetailsModel command);
    }
}
