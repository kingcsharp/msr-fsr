using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkflowGroup))]
    public partial class WorkflowGroup: TrackableEntity
    {
        [StringLength(100)]
        public string Name { get; set; }

        public bool? IsActive { get; set; }
    }
}
