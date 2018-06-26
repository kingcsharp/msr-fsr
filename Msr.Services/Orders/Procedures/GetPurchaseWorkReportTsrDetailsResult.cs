
using System;

namespace Msr.Services.Orders.Procedures
{
    public class GetPurchaseWorkReportTsrDetailsResult
    {
        public string PurchaseId { get; set; }

        public string PurchaseItemId { get; set; }

        public string ActualPartDbId { get; set; }

        public string LineItem { get; set; }

        public string Quantity { get; set; }

        public string Serial { get; set; }

        public string SupplierName { get; set; }

        public string CustomerName { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public string OwnerPartName { get; set; }

        public DateTime FillDate { get; set; }

        public string CustomerPurchaseNumber { get; set; }

        public string AccountNumber { get; set; }

        public string BlanketPoNumber { get; set; }

        public string AccountId { get; set; }
    }
}
