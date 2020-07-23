using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateWorkflowStageModel : Command
    {
        public CreateWorkflowStageModel()
        {
            WorkflowGroupStageMapModel = new HashSet<WorkflowGroupStageMapModel>() { };
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<WorkflowGroupStageMapModel> WorkflowGroupStageMapModel { get; set; }
    }
}
