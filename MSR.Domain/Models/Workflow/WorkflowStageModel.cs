using MSR.Domain.Models.BaseModels;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class WorkflowStageModel : DeletableModel
    {
        public WorkflowStageModel()
        {
            Groups = new HashSet<WorkflowGroupStageMapModel>();
        }
        public string Name { get; set; }
        public string LastUpdatedByName { get; set; }
        public string CreatedByName { get; set; }
        public ICollection<WorkflowGroupStageMapModel> Groups { get; set; }
    }
}
