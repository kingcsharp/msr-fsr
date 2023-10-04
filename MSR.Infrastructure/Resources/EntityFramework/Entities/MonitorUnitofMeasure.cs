using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(MonitorUnitofMeasure))]
    public class MonitorUnitofMeasure: Entity
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
    }
}