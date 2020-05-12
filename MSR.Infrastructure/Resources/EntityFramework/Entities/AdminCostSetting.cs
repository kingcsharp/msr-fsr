using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(AdminCostSetting))]
    public partial class AdminCostSetting: Entity
    {
        [Key]
        [Column(Order = 1)]
        public decimal RMAnnualRate { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal LaborRateMinute { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int YearsHours { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HourMinutes { get; set; }
    }
}
