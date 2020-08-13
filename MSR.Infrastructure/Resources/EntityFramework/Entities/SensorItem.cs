using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table("SensorItemNameMap")]
    public class SensorItem: Entity
    {
        public string SensorName { get; set; }
        public string ItemId { get; set; }
        public virtual int? LocationId { get; set; }
        public virtual Location AssignedLocation { get; set; }
    }
}
