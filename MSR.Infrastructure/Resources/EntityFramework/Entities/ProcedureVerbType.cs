using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureVerbType))]
    public partial class ProcedureVerbType: TrackableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
