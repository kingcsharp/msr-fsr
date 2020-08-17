using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class Sensor: CreatableEntity
    {
        public string SensorName { get; set; }
        public virtual int? SiteId { get; set; }
        public virtual Location Site { get; set; }
        public virtual int? AssignedLocationId { get; set; }
        public virtual Location AssignedLocation { get; set; }

    }
}
