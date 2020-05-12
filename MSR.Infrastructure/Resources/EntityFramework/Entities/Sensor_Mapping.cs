using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public partial class Sensor_Mapping
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SensorMappingID { get; set; }

        [StringLength(50)]
        public string Site { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SensorName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(250)]
        public string MonitoringID { get; set; }

        public int? LocationId { get; set; }
    }
}
