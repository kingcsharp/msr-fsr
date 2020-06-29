using MSR.Domain.Commanding;
using MSR.Domain.Models;
using MSR.Domain.Models.Workflow;
using System.Collections.Generic;

namespace MSR.Domain.Commands.Workflow
{
    public class CreateWorkflowModel : Command
    {
        public CreateWorkflowModel()
        {
            MemberStages = new List<WorkflowStageMapModel>() { };
            ActivityMaps = new List<WorkflowActivityMapModel>() { };
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<WorkflowStageMapModel> MemberStages { get; set; }
        public ICollection<WorkflowActivityMapModel> ActivityMaps { get; set; }
    }
}