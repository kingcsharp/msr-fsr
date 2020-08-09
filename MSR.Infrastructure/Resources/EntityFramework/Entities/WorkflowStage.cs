using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkflowStage))]
    public partial class WorkflowStage : TrackableEntity
    {
        public WorkflowStage()
        {
            Group = new HashSet<WorkflowGroupStageMap>();
        }

        [StringLength(50)]
        public string Name { get; set; }

        public bool? IsActive { get; set; }
        public virtual ICollection<WorkflowGroupStageMap> Group { get; set; }
    }
}
