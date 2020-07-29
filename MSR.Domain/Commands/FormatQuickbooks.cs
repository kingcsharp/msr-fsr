using System.Collections.Generic;
using MSR.Domain.Commanding;
using MSR.Domain.Models;

namespace MSR.Domain.Commands
{
    public class FormatQuickbooks : Command
    {
        public string FormatType { get; set; }
        public IEnumerable<InvoiceModel> Invoices { get; set; }
    }
}
