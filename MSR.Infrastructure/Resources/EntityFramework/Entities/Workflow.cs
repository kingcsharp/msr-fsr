using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Workflow))]
    public partial class Workflow : DeletableEntity
    {
        public Workflow()
        {
            MemberStages = new HashSet<WorkflowStageMap>();
            ActivityMaps = new HashSet<WorkflowActivityMap>();
        }

        [StringLength(100)]
        public string Name { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User Created { get; set; }

        [ForeignKey("LastUpdatedBy")]
        public virtual User LastUpdated { get; set; }

        public virtual ICollection<WorkflowStageMap> MemberStages { get; set; }
        public virtual ICollection<WorkflowActivityMap> ActivityMaps { get; set; }
    }
}
