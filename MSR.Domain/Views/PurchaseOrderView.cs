using System;
using System.Collections.Generic;

namespace MSR.Domain.Views
{
    public class PurchaseOrderView
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Name { get; set; }

        public string CustomerReferencePO { get; set; }

        public string CustomerReferenceNo { get; set; }

        public int? InvoicedBalance { get; set; }

        public int? UninvoicedBalance { get; set; }

        public int? Balance { get; set; } 

        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public decimal? TotalPurchaseLimit { get; set; }

        public string CustomerReference { get; set; }

        public virtual ICollection<PurchaseOrderProductView> Products { get; set; }
        public bool IsDeletable { get; set; }
        public string Status { get; set; }
    }
}
