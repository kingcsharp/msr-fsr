using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class CreateUpdateInvoiceItem
    {
        public int PurchaseOrderId { get; set; }

        public int WorkOrderId { get; set; }
    }
}
