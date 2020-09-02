using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class CreatePurchaseOrder: Command
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string CustomerReferencePO { get; set; }
        public int[] Products { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string CustomerReferenceNo { get; set; }
        public decimal TotalPurchaseLimit { get; set; }
        public decimal Tax { get; set; }
        public string ReferenceName { get; set; }

    }
}
