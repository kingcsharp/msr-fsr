
using System;

namespace MSR.Answer.API.V1.Models
{
    public class GetInvoicesRequest
    {
        public int? Id { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedByName { get; set; }
        public decimal? Amount { get; set; }
        public int? StatusId { get; set; }
    }
}
