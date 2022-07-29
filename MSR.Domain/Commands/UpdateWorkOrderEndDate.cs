using System;
using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UpdateWorkOrderEndDate : Command
    {
        public int WorkOrderId { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public string ScheduledEndDateChangeReason { get; set;  }
    }
}
