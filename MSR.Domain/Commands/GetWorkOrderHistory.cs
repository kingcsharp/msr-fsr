using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class GetWorkOrderHistory : PagingCommand
    {
        public int? PurchaseId { get; set; }
        public string WorkOrderItemNumber { get; set; }
        public string Customer { get; set; }
        public string[] Location { get; set; }
        public string SerialNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int? Qty { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string Product { get; set; }
        public string Procedure { get; set; }
        public string[]? Status { get; set; }
        public string Dispostion { get; set; }
    }
}
