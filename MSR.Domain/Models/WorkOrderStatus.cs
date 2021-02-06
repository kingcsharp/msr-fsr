using System.ComponentModel.DataAnnotations;

namespace MSR.Domain.Models
{
    public class WorkOrderStatus
    {
        public string ProductName { get; set; }
        public string PartNumber { get; set; }
        public string ProcedureName { get; set; }
        public string LocationName { get; set; }
        public WorkOrderSummary WorkOrderSummary { get; set; }
    }
}
