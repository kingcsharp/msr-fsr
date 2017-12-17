using System;

namespace Msr.Models.Orders
{
    public class PurchaseView
    {
        public string ObjectId { get; set; }
        public string CustPurchNum { get; set; }
        public string Description { get; set; }
        public string PurchaseStatus { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string Root { get; set; }
        public string Id { get; set; }
    }
}
