using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table("PortalWorkOrderView")]
    public class PortalWorkOrder: Entity
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string SerialNumber { get; set; }
        public string CompanyPartNumber { get; set; }
        public int? CycleCount { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int? Qty { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string PartName { get; set; }
        public int? PartId { get; set; }
        public string ProductName { get; set; }
        public string ProcedureName { get; set; }
        public string Status { get; set; }
        public decimal? Price { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceName { get; set; }
        public int? WorkOrderTaskId { get; set; }
    }
}
