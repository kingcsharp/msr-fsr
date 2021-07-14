using System;
namespace MSR.Domain.Views
{
    public class WorkOrderSelectItem
    {
        public int WorkOrderId { get; set; }
        public string CustomerPurchaseNumber { get; set; }
        public string ProcedureName { get; set; }
        public string PurchaseSerialNumber { get; set; }
        public string WorkOrderStatusId { get; set; }

    }
}
