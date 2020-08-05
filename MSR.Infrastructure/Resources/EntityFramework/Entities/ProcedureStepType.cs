using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepType))]
    public partial class ProcedureStepType: Entity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
