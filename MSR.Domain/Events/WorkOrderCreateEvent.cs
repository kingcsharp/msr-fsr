using System;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Events
{
    public class WorkOrderCreateEvent: BaseImportEvent
    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int PurchaseOrderId { get; set; }
        public decimal Price { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public bool HasNCR { get; set; }
        public int LocationId { get; set; }
        public int Qty { get; set; }
        public bool SerializeIndividually { get; set; }
        public ICollection<string> SerialNumbers { get; set; }
        public ICollection<string> CustomerLineNumbers { get; set; }
    }
}
