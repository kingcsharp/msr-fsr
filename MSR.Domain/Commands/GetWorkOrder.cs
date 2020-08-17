using MSR.Domain.Commanding;
using System;

namespace MSR.Domain.Commands
{
    public class GetWorkOrder : Command
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public int? LocationId { get; set; }
        public DateTime? InvoiceDate { get; set; }
    }
}
