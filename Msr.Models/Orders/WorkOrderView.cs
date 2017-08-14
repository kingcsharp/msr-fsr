using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace Msr.Models.Orders
{
    public class WorkOrderView
    {
        public Guid Id { get; set; }
        public string RequesteeName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierId { get; set; }
        public int? HasFile { get; set; }
        public int? HasMonitor { get; set; }
        public int? HasNcr { get; set; }
        public string TaskId { get; set; }
        public double? MyTotHours { get; set; }
        public double? MyCompHours { get; set; }
        public string CurStepText { get; set; }
        public double? TimeComplete { get; set; }
        public double? PercComplete { get; set; }
        public string BatchParent { get; set; }
        public byte? Batched { get; set; }
        public int? BatchedFill { get; set; }
        public string FillItemId { get; set; }
        public string NickName { get; set; }
        public string Serial { get; set; }
        public string PurchaseId { get; set; }
        public string PurchaseHistId { get; set; }
        public string PurchaseItemId { get; set; }
        public string CustomerName { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? OrigDueDate { get; set; }
        public string ActualPartId { get; set; }
        public string ActPartObjId { get; set; }
        public DateTime? StDate { get; set; }
        public string ProcId { get; set; }
        public string CustId { get; set; }
        public DateTime? StartDate { get; set; }
        public string RequesteeId { get; set; }
        public string GroupRequesteeId { get; set; }
        public string ProductName { get; set; }
        public string CustPurchNum { get; set; }
        public string ReferencePo { get; set; }
        public string ProcName { get; set; }
        public double? Qty { get; set; }
        public Single FillQty { get; set; }
        public string Status { get; set; }
        public string FillId { get; set; }
        public string MtNum { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualStopDate { get; set; }
        public string Notes { get; set; }
        public int? Threshold { get; set; }
        [NotMapped]
        public string Action { get; set; }
    }
}
