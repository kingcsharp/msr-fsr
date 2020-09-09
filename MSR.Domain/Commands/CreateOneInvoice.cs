using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateOneInvoice : Command
    {
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceClass { get; set; }
        public decimal? TaxPercentage { get; set; }
        public ICollection<CreateUpdateInvoiceItem> InvoiceItems { get; set; }
    }
}
