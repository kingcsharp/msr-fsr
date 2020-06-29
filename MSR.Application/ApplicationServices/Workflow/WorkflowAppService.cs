using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class WorkflowAppService :
            ICommandHandler<GetPendingApprovals>
    {
        private IWorkflowService _workflowService;
        public WorkflowAppService(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        public async Task<ICommandResponse> HandleAsync(GetPendingApprovals command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.GetApprovalNotificationsAsync(command);
            return new CommandResponse<PendingApprovalNotification>(ret);
        }
    }
}
