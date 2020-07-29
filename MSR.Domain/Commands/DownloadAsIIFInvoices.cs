using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class DownloadAsIIFInvoices : Command
    {
        public string InvoiceNumber { get; set; }
        public int? CustomerId { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal? Total { get; set; }
        public int? StatusId { get; set; }
    }
}
