using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices.Workflow
{
    public class WorkflowGroupAppService: ICommandHandler<GetWorkflowGroupsModel>,
        ICommandHandler<CreateWorkflowGroup>,
        ICommandHandler<UpdateWorkflowGroupModel>,
        ICommandHandler<DeactivateWorkflow>
    {
        private IWorkflowGroupService _workflowService;
        public WorkflowGroupAppService(IWorkflowGroupService workflowService)
        {
            _workflowService = workflowService;
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkflowGroupsModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.GetWorkFlowGroupsAsync(command);
            return new CommandResponse<ICollection<WorkflowGroupModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkflowGroup command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.CreateWorkFlowGroupAsync(command);
            return new CommandResponse<WorkflowGroupModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkflowGroupModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.UpdateWorkFlowGroupAsync(command);
            return new CommandResponse<WorkflowGroupModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeactivateWorkflow command, CancellationToken cancellationToken = default)
        {
            await _workflowService.DeactivateWorkFlowGroupAsync(command);
            return new CommandResponse();
        }

    }
}
