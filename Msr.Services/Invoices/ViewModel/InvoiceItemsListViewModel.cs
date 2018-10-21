using System;

namespace Msr.Services.Invoices.ViewModel
{
    public class InvoiceItemsListViewModel
    {
        public string FillItemId { get; set; }

        public string PurchaseId { get; set; }

        public string CustPurchNum { get; set; }

        public string OpenDate { get; set; }

        public string PurchaseItemId { get; set; }

        public string Description { get; set; }

        public Single? FillQty { get; set; }

        public Single? TotalSalePrice { get; set; }

        public double? Amount { get; set; }
    }
}
