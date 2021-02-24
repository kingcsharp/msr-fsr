using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrderStatusSummary))]
    public class WorkOrderStatusSummary: Entity
    {
        public int WorkOrderId { get; set; }
        public string WorkOrderItem { get; set; }
        public string PurchaseOrderLineNumber { get; set; }
        public string WorkOrderPartSerialNumber { get; set; }
        public string WorkOrderStatus { get; set; }
        public string WorkOrderAssignedTo { get; set; }
        public int AssignedTo { get; set; }
        public bool WorkOrderHasNCR { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public string LocationName { get; set; }
        public string ProductName { get; set; }
        public string PartNumber { get; set; }
        public string ProcedureName { get; set; }
    }
}
