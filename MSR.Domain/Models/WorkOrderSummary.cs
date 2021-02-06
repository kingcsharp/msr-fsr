using System;

namespace MSR.Domain.Models
{
    public class WorkOrderSummary
    {
        public int? WorkOrderId { get; set; }
        public string WorkOrderItemNumber { get; set; }
        public string PurchaseOrderLineNumber { get; set; }
        public string WorkOrderPartSerialNumber { get; set; }
        public string WorkOrderStatus { get; set; }
        public string WorkOrderAssignedTo { get; set; }
        public int? AssignedTo { get; set; }
        public bool WorkOrderHasNcr { get; set; }
        public DateTime? WorkOrderScheduledEndDate { get; set; }
    }
}
