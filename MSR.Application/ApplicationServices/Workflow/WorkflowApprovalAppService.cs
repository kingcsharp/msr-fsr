using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices.Workflow
{
    public class WorkflowApprovalAppService: 
        ICommandHandler<GetPendingApprovalModel>,
        ICommandHandler<PostApprovalModel>,
        ICommandHandler<DeactivateApprovalModel>,
        ICommandHandler<GetPendingApprovalDetailsModel>
    {
        private readonly IWorkflowApprovalService _workflowApprovalService;
        public WorkflowApprovalAppService(IWorkflowApprovalService workflowApprovalService)
        {
            _workflowApprovalService = workflowApprovalService;
        }

        public async Task<ICommandResponse> HandleAsync(GetPendingApprovalDetailsModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowApprovalService.GetApprovalChangesAsync(command);
            return new CommandResponse<PendingApprovalPopoverModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetPendingApprovalModel command, CancellationToken cancellationToken = default)
        {
            var pendingApprovalModels = await _workflowApprovalService.GetPendingApprovalAsync(command);
            int totalRows = pendingApprovalModels.AsQueryable().CreateWorkflowPendingQuery(command).Count();
            pendingApprovalModels = pendingApprovalModels.AsQueryable().CreateWorkflowPendingQuery(command).ToList();
            return new PagingCommandResponse<ICollection<PendingApprovalModel>>(pendingApprovalModels, totalRows, command.Term, command.PageNumber, command.PageSize, command.SortAscending);
        }

        public async Task<ICommandResponse> HandleAsync(PostApprovalModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowApprovalService.CreateApprovalAsync(command);
            return new CommandResponse<PendingApprovalModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeactivateApprovalModel command, CancellationToken cancellationToken = default)
        {
            await _workflowApprovalService.DeactivateApprovalAsync(command);
            return new CommandResponse();
        }
    }
}
