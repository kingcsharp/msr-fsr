namespace MSR.Domain.Models
{
    public class AdminCostSettingsModel
    {
        public decimal RMAnnualRate { get; set; }
        public decimal LaborRateMinute { get; set; }
        public int YearsHours { get; set; }
        public int HourMinutes { get; set; }
    }
}
