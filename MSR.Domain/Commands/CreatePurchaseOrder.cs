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
        public string ReferenceCustomerPO { get; set; }
        public int[] Products { get; set; }
        public DateTimeOffset OpenDate { get; set; }
        public DateTimeOffset CloseDate { get; set; }
        public string CustomerReferenceNo { get; set; }
        public decimal TotalPurchaseLimit { get; set; }
        public decimal Tax { get; set; }
    }
}
