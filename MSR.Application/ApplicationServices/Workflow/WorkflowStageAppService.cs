using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Exceptions;
using MSR.Domain.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices.Workflow
{
    public class WorkflowStageAppService : ICommandHandler<GetWorkflowStageModel>,
        ICommandHandler<CreateWorkflowStageModel>,
        ICommandHandler<UpdateWorkflowStageModel>,
        ICommandHandler<DeactivateWorkflowStage>
    {
        private IWorkflowStageService _workflowStageService;
        public WorkflowStageAppService(IWorkflowStageService workflowStageService)
        {
            _workflowStageService = workflowStageService;
        }

        public async Task<ICommandResponse> HandleAsync(GetWorkflowStageModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowStageService.GetWorkFlowStageAsync(command);
            return new CommandResponse<ICollection<WorkflowStageModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateWorkflowStageModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowStageService.CreateWorkFlowStageAsync(command);
            return new CommandResponse<WorkflowStageModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateWorkflowStageModel command, CancellationToken cancellationToken = default)
        {
            var ret = await _workflowStageService.UpdateWorkFlowStageAsync(command);
            return new CommandResponse<WorkflowStageModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeactivateWorkflowStage command, CancellationToken cancellationToken = default)
        {
            await _workflowStageService.DeactivateWorkFlowStageAsync(command);
            return new CommandResponse();
        }
    }
}
