using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class UpdateAdminCostSetting : Command
    {
        public decimal? RMAnnualRate { get; set; }
        public decimal? LaborRateMinute { get; set; }
        public int? YearsHours { get; set; }
        public int? HourMinutes { get; set; }
    }
}
