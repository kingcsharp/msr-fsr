using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.DTOs
{
    public class WorkOrderHistoryViewDTO
    {
        public int? Id { get; set; }
        public int? PurchaseId { get; set; }
        public string WorkOrderItemNumber { get; set; }
        public string CustomerName { get; set; }
        public string LocationName { get; set; }
        public string SerialNumber { get; set; }
        public int? PurchaseOrderNumber { get; set; }
        public string ReferencePO { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string ProductName { get; set; }
        public ICollection<WorkOrderHistoryTaskDTO> WorkOrderTasks { get; set; }
        public  WorkOrderHistoryPartDTO WorkOrderPart { get; set; }
        public bool HasNcr { get; set; }
    }
}
