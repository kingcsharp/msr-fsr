using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class GetWorkOrderHistoryRequest : BaseApiModel
    {
        public int? PurchaseId { get; set; }
        public string WorkOrderItemNumber { get;set;}
        public string Customer { get; set; }
        public string Location { get; set; }
        public string SerialNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int? Qty { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string Product { get; set; }
        public string Procedure { get; set; }
        public string Status { get; set; }
        public string Dispostion { get; set; }
        
    }
}
