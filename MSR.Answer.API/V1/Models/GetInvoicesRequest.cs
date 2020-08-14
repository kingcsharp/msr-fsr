
using System;

namespace MSR.Answer.API.V1.Models
{
    public class GetInvoicesRequest
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public string Description { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? Total { get; set; }
        public int? StatusId { get; set; }
    }
}
