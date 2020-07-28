
using System;

namespace MSR.Answer.API.V1.Models
{
    public class DownloadInvoicesRequest
    {
        public int? CustomerId { get; set; }
        public string? Description { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? Total { get; set; }
        public int? StatusId { get; set; }
    }
}
