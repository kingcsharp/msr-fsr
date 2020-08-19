using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class GetInvoices : Command
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
