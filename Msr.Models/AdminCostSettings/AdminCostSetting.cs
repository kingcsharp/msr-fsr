using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.AdminCostSettings
{
    public class AdminCostSetting
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Repair & Maintenance Annual Rate")]
        public decimal RmAnnualRate { get; set; }

        [Required]
        [DisplayName("Labor Rate Per Minute")]
        public decimal LaborRateMinute { get; set; }

        [Required]
        [DisplayName("Year Hours")]
        public int YearsHours { get; set; }

        [Required]
        [DisplayName("Hour Minutes")]
        public int HourMinutes { get; set; }
    }
}
