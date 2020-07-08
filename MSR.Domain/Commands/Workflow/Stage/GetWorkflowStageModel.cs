using MSR.Domain.Commanding;
using MSR.Domain.Models.Workflow;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetWorkflowStageModel : Command<ICollection<WorkflowStageModel>>
    {
        public int? Id { get; set; }
    }
}
