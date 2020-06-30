using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices.Workflow
{
    public class WorkflowGroupAppService: ICommandHandler<GetWorkflowGroupsModel>,
        ICommandHandler<CreateWorkflowGroupModel>,
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

        public async Task<ICommandResponse> HandleAsync(CreateWorkflowGroupModel command, CancellationToken cancellationToken = default)
        {
            try {
                var ret = await _workflowService.CreateWorkFlowGroupAsync(command);
                return new CommandResponse<WorkflowGroupModel>(ret);
            } catch (DbUpdateException e) {
                return new CommandResponse<WorkflowGroupModel>(
                    new DomainException(
                        e.InnerException.Message,
                        Domain.Commanding.Enums.DomainError.NotFound
                    )
                );
            }
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkflowGroupModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowService.UpdateWorkFlowGroupAsync(command);
            if (ret == null) {
                return new CommandResponse<WorkflowGroupModel>(
                    new DomainException(
                        "Workflow Group Model not found",
                        Domain.Commanding.Enums.DomainError.NotFound
                    )
                );
            } else {
                return new CommandResponse<WorkflowGroupModel>(ret);
            }
        }

        public async Task<ICommandResponse> HandleAsync(DeactivateWorkflow command, CancellationToken cancellationToken = default)
        {
            await _workflowService.DeactivateWorkFlowGroupAsync(command);
            return new CommandResponse();
        }

    }
}
