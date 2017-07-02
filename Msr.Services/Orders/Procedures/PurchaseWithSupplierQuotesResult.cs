using System;

namespace Msr.Services.Orders.Procedures
{
    public class PurchaseWithSupplierQuotesResult
    {
        public string PurchaseNumber { get; set; }
        public string ShipFrom { get; set; }
        public string CustomerPo { get; set; }
        public DateTime ShipDate { get; set; }
    }
}
