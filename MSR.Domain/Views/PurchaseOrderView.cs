using MSR.Domain.Models.BaseModels;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Views
{
    public class PurchaseOrderView : CreatableModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
        public string ReferenceName { get; set; }

        public string Name { get; set; }

        public string CustomerReferencePO { get; set; }

        public string CustomerReferenceNo { get; set; }

        public int InvoicedBalance { get; set; }

        public int UninvoicedBalance { get; set; }

        public int Balance { get; set; } 

        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public decimal TotalPurchaseLimit { get; set; }

        public string CustomerReference { get; set; }

        public virtual ICollection<PurchaseOrderProductView> Products { get; set; }
        public bool IsDeletable { get; set; }
        public string Status { get; set; }
        public int? Revision { get; set; }
        public double UnusedAmount { get; set; }
    }
}
