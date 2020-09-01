using System;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class PurchaseOrderModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string ReferencePO { get; set; }

        public string ReferenceName { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public decimal? TotalPurchaseLimit { get; set; }

        public string CustomerReference { get; set; }

        public int LocationId { get; set; }

        public virtual LocationModel Location { get; set; }
    }
}
