using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkflowActivity))]
    public partial class WorkflowActivity: TrackableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string ApprovalTableName { get; set; }

        public bool? IsActive { get; set; }

        public bool? CreateRevision { get; set; }
    }
}
