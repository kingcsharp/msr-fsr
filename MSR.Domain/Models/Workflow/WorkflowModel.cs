using MSR.Domain.Models.BaseModels;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class WorkflowModel : DeletableModel
    {
        public WorkflowModel()
        {
            MemberStages = new HashSet<WorkflowStageMapModel>();
            ActivityMaps = new HashSet<WorkflowActivityMapModel>();
        }

        public string Name { get; set; }
        public string LastUpdatedByName { get; set; }
        public string CreatedByName { get; set; }
        public ICollection<WorkflowStageMapModel> MemberStages { get; set; }
        public ICollection<WorkflowActivityMapModel> ActivityMaps { get; set; }
    }
}
