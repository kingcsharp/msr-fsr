using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices.Workflow
{
    public class WorkflowApprovalAppService: 
        ICommandHandler<GetPendingApproval>,
        ICommandHandler<PostApprovalModel>,
        ICommandHandler<DeactivateApprovalModel>
    {
        private readonly IWorkflowApprovalService _workflowApprovalService;
        public WorkflowApprovalAppService(IWorkflowApprovalService workflowApprovalService)
        {
            _workflowApprovalService = workflowApprovalService;
        }

        public async Task<ICommandResponse> HandleAsync(GetPendingApproval command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowApprovalService.GetPendingApprovalAsync(command);
            return new CommandResponse<ICollection<PendingApprovalModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(PostApprovalModel command, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
            await _workflowApprovalService.CreateApprovalAsync(command);
            return new CommandResponse();
        }

        public async Task<ICommandResponse> HandleAsync(DeactivateApprovalModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowApprovalService.DeactivateApprovalAsync(command);
            return new CommandResponse<PendingApprovalModel>(ret);
        }
    }
}
