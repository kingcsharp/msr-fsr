using System;

namespace MSR.Answer.API.V1.Models
{
    public class GetWorkOrderRequest
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public int? LocationId { get; set; }
        public DateTime? InvoiceDate { get; set; }
    }
}
