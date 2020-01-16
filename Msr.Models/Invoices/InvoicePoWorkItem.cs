using System;

namespace Msr.Models.Invoices
{
    public class InvoicePoWorkItem
    {
        public string TaskId { get; set; }

        public string FillItemId { get; set; }

        public string PurchaseId { get; set; }

        public string CustPurchNum { get; set; }

        public string OpenDate { get; set; }

        public string PurchaseItemId { get; set; }

        public string Description { get; set; }

        public Single? FillQty { get; set; }

        public Single? TotalSalePrice { get; set; }

        public decimal? Amount { get; set; }

        public string Status { get; set; }

        public string Purchaser { get; set; }

        public string LocationName { get ; set; }
    }
}
