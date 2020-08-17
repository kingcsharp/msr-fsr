using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(SensorValue))]
    public class SensorValue: CreatableEntity
    {
        public int? SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }
        public string ItemCurrentValue { get; set; }
        public string AlarmDescription { get; set; }
        public bool? IsAlarm { get; set; }
    }
}
