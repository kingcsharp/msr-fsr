using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetWorkflowStageModel : Command<ICollection<WorkflowStageModel>>
    {
        public int? Id { get; set; }
    }
}
