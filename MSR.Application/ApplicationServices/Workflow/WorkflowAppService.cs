using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class WorkflowAppService :
            ICommandHandler<GetPendingApprovals>,
        ICommandHandler<GetWorkflowModel>,
        ICommandHandler<CreateWorkflowModel>,
        ICommandHandler<UpdateWorkflowModel>,
        ICommandHandler<DeactivateWorkflowModel>,
        ICommandHandler<GetWorkflowActivities>
    {
        private readonly IWorkflowService _workflowService;
        public WorkflowAppService(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        public async Task<ICommandResponse> HandleAsync(GetPendingApprovals command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.GetApprovalNotificationsAsync(command);
            return new CommandResponse<PendingApprovalNotification>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkflowActivities command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.GetWorkFlowAsync(command);
            return new CommandResponse<ICollection<WorkflowActivityModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkflowModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.GetWorkFlowAsync(command);
            return new CommandResponse<ICollection<WorkflowModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkflowModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.CreateWorkFlowAsync(command);
            return new CommandResponse<WorkflowModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkflowModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.UpdateWorkFlowAsync(command);
            return new CommandResponse<WorkflowModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeactivateWorkflowModel command, CancellationToken cancellationToken = default)
        {
            await _workflowService.DeactivateWorkFlowAsync(command);
            return new CommandResponse();
        }
    }
}
