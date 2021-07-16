using System;

namespace MSR.Domain.Views
{
    public class InvoiceableWorkOrderView
    {
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }//
        public string ReferencePO { get; set; }
        public string CustomerPurchaseNumber { get; set; }
        public string SerialNumber { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal TotalSalePrice { get; set; }
        public int? CustomerLineNumber { get; set; }
    }
}
