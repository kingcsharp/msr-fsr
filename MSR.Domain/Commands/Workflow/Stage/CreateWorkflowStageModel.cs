using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateWorkflowStageModel : Command
    {
        public CreateWorkflowStageModel()
        {
            WorkflowGroupStageMapModel = new List<WorkflowGroupStageMapModel>() { };
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<WorkflowGroupStageMapModel> WorkflowGroupStageMapModel { get; set; }
    }
}
