using MSR.Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Models.Workflow
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
        public virtual ICollection<WorkflowStageMapModel> MemberStages { get; set; }
        public virtual ICollection<WorkflowActivityMapModel> ActivityMaps { get; set; }
    }
}
