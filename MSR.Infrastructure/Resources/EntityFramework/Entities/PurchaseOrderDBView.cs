using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class PurchaseOrderDBView
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
        public string? ReferenceName { get; set; }

        public string? Name { get; set; }

        public string CustomerReferencePO { get; set; }

        public string CustomerReferenceNo { get; set; }

        public decimal InvoicedBalance { get; set; }

        public decimal UninvoicedBalance { get; set; }

        public decimal Balance { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public decimal TotalPurchaseLimit { get; set; }

        public string CustomerReference { get; set; }

        public bool IsDeletable { get; set; }
        public string Status { get; set; }
        public int? Revision { get; set; }
        public decimal UnusedAmount { get; set; }
    }
}
