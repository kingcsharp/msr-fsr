using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(AdminCostSetting))]
    public partial class AdminCostSetting: Entity
    {
        public decimal RMAnnualRate { get; set; }

        public decimal LaborRateMinute { get; set; }

        public int YearsHours { get; set; }

        public int HourMinutes { get; set; }
    }
}
