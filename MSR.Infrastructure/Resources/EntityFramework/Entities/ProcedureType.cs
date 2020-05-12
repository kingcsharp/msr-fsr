using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureType))]
    public partial class ProcedureType: TrackableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int? ProcedureVerbTypeId { get; set; }

        public virtual ProcedureVerbType ProcedureVerbType { get; set; }
    }
}
