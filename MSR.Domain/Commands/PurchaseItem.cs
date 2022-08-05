using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class PurchaseItem
    {
        public List<string> CustomerLineNumbers { get; set; }
        public DateTime DueDate { get; set; }
        public int LocationId { get; set; }
        public string MTTN { get; set; }
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderProductId { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Qty { get; set; }
        public bool SerializeIndividually { get; set; }
        public List<string> SerialNumbers { get; set; }
        public int StatusId { get; set; }
        public string CustomerPurchaseNumber { get; set; }
    }
}
