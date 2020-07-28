using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class UpdateInvoice : Command
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal? TaxPercentage { get; set; }
        public int StatusId { get; set; }
        public ICollection<CreateUpdateInvoiceItem> InvoiceItems { get; set; }
    }
}
