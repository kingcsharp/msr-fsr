using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class DownloadAsIIFInvoices : Command
    {
        public int? Id { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedByName { get; set; }
        public decimal? Total { get; set; }
        public int? StatusId { get; set; }
    }
}
