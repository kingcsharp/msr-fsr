using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Workflow))]
    public class Workflow : DeletableEntity
    {
        public Workflow()
        {
            MemberStages = new HashSet<WorkflowStageMap>();
            ActivityMaps = new HashSet<WorkflowActivityMap>();
        }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<WorkflowStageMap> MemberStages { get; set; }
        public virtual ICollection<WorkflowActivityMap> ActivityMaps { get; set; }
    }
}
