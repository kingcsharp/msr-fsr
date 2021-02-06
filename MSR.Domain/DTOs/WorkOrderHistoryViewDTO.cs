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
        public int? PurchaseOrderNumber { get; set; }
        public string ReferencePO { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string ProductName { get; set; }
        public ICollection<WorkOrderTaskDTO> WorkOrderTasks { get; set; }
        public ICollection<WorkOrderMessageDTO> WorkOrderMessages { get; set; }
        public  WorkOrderPartDTO WorkOrderPart { get; set; }
        public bool HasNcr { get; set; }
    }
}
