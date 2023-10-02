using System.Collections.Generic;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(MonitorUnitofMeasure))]
    public class MonitorUnitofMeasure: Entity
    {
        public virtual int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }
    }
}