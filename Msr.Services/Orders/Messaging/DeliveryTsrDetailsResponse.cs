using System.Collections.Generic;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class DeliveryTsrDetailsResponse
    {
        public DeliveryTsrDetailsResponse()
        {
            PurchaseWithSupplierQuotesResult = new PurchaseWithSupplierQuotesResult();
            PurchaseItemInfoResult = new PurchaseItemInfoResult();
            PurchaseItemInfoResultList = new List<PurchaseItemInfoResult>();
        }

        public int FillId { get; set; }
        public PurchaseWithSupplierQuotesResult PurchaseWithSupplierQuotesResult { get; set; }
        public PurchaseItemInfoResult PurchaseItemInfoResult { get; set; }
        public List<PurchaseItemInfoResult> PurchaseItemInfoResultList { get; set; }
    }
}
