using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class UpdatePurchaseOrder: CreatePurchaseOrder
    {
        public int Id { get; set; }
    }
}
