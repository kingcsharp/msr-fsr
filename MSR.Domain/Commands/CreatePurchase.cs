using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class CreatePurchase : Command
    {
        public int PurchaseOrderId { get; set; }
        public int StatusId { get; set; }
        public int PurchaseOrderProductId { get; set; }
        public string CustomerPurchaseNumber { get; set; }
        public int LocationId { get; set; }
        public string SerialNumber { get; set; }
        public int Qty { get; set; }
        public int CustomerLineNumber { get; set; }
        public string MTTN { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public bool SerializeIndividually { get; set; }
    }
}
