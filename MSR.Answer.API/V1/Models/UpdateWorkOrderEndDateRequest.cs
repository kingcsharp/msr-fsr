using System;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateWorkOrderEndDateRequest
    {
        public int WorkOrderId { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public string ScheduledEndDateChangeReason { get; set; }
    }
}