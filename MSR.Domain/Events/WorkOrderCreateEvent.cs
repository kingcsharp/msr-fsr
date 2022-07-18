using System;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Events
{
    public class WorkOrderCreateEvent: BaseImportEvent
    {
        public WorkOrderCreateEvent()
        {
            WorkOrderProducts = new List<WorkOrderProduct>();
        }
        public int PurchaseId { get; set; }
        public ICollection<WorkOrderProduct> WorkOrderProducts { get; set; }
        public int PurchaseOrderId { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public bool HasNCR { get; set; }
        public int LocationId { get; set; }
    }
}
