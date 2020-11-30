using MSR.Domain.Models.BaseModels;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Views
{
    public class PurchaseOrderView : CreatableModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
        public string? ReferenceName { get; set; }

        public string? Name { get; set; }

        /// <summary>
        /// CustomerReferencePO aka ReferencePO
        /// </summary>
        public string CustomerReferencePO { get; set; }

        /// <summary>
        /// CustomerReferenceNo aka CustomerReference aka MTTN
        /// </summary>
        public string CustomerReferenceNo { get; set; }

        public decimal InvoicedBalance { get; set; }

        public decimal UninvoicedBalance { get; set; }

        public decimal Balance { get; set; } 

        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public decimal TotalPurchaseLimit { get; set; }

        public string CustomerReference { get; set; }

        public virtual ICollection<PurchaseOrderProductView> Products { get; set; }
        public bool IsDeletable { get; set; }
        public string Status { get; set; }
        public int? Revision { get; set; }
        public decimal UnusedAmount { get; set; }
    }
}
