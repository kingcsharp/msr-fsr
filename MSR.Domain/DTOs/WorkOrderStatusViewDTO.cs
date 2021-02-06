using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.DTOs
{
    public class WorkOrderStatusViewDTO
    {
        public string ProductName { get; set; }
        public string LocationName { get; set; }
        public int? WorkOrderId { get; set; }
        public string WorkOrderItemNumber { get; set; }
        public string PurchaseOrderLineNumber { get; set; }
        public bool WorkOrderHasNcr { get; set; }
        public DateTime? WorkOrderScheduledEndDate { get; set; }
        public ICollection<WorkOrderTaskDTO> WorkOrderTasks { get; set; }
        public WorkOrderPartDTO WorkOrderPart { get; set; }
    }
}
