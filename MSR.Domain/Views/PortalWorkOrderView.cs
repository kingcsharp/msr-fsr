using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class PortalWorkOrderView
    {
        public int CustomerId { get; set; }
        public int WorkOrderItemNumber { get; set; }
        public string SerialNumber { get; set; }
        public string CompanyPartNumber { get; set; }
        public int CycleCount { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int Qty { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public string PartName { get; set; }
        public int? PartId { get; set; }
        public string ProductName { get; set; }
        public string ProcedureName { get; set; }
        public string Status { get; set; }
        public bool HasPhotos { get; set; }
        public bool HasNCRs { get; set; }
        public bool HasFiles { get; set; }
        public bool HasMonitors { get; set; }
        public string Disposition { get; set; }
        public string Supplier { get; set; }
        public double? Price { get; set; }
        public double? InvoiceAmount { get; set; }
        public DateTimeOffset? InvoiceDate { get; set; }
        public string InvoiceName { get; set; }
    }
}
