using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models.Workflow
{
    public class WorkflowStageModel : DeletableModel
    {
        public string Name { get; set; }
        public string LastUpdatedByName { get; set; }
        public string CreatedByName { get; set; }
    }
}
