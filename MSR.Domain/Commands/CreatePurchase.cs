using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreatePurchase : Command
    {
        public ICollection<PurchaseItem> PurchaseRequests { get; set; }
        public bool GroupLines { get; set; }
    }
}
