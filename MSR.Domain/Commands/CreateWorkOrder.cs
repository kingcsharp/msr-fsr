using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateWorkOrder : Command
    {
        public int PurchaseId { get; }
        public int ProductId { get; }
        public int PurchaseOrderId { get; }
        public decimal Price { get; }
        public DateTime ScheduledStartDate { get; }
        public DateTime ScheduledEndDate { get; }
        public bool HasNCR { get; }
        public int LocationId { get; }
        public int Qty { get; }
        public bool SerializeIndividually { get; }
        public ICollection<string> SerialNumbers { get; }
        public ICollection<string> CustomerLineNumbers { get; }

        public CreateWorkOrder(int purchaseId, int productId, int purchaseOrderId, decimal price, DateTime scheduledStartDate,
            DateTime scheduledEndDate, bool hasNcr, int locationId, int qty, bool serializeIndividually, 
            ICollection<string> serialNumbers, ICollection<string> customerLineNumbers)
        {
            PurchaseId = purchaseId;
            ProductId = productId;
            PurchaseOrderId = purchaseOrderId;
            Price = price;
            ScheduledEndDate = scheduledEndDate;
            ScheduledStartDate = scheduledStartDate;
            HasNCR = hasNcr;
            LocationId = locationId;
            Qty = qty;
            SerializeIndividually = serializeIndividually;
            SerialNumbers = serialNumbers;
            CustomerLineNumbers = customerLineNumbers;
        }

    }
}
