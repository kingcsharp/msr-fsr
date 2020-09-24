namespace MSR.Answer.API.V1.Models
{
    public class UpdateAdminCostSettingRequest
    {
        public decimal? RMAnnualRate { get; set; }
        public decimal? LaborRateMinute { get; set; }
        public int? YearsHours { get; set; }
        public int? HourMinutes { get; set; }
    }
}
