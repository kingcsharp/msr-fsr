using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateQuote : Command
    {
        public int CustomerId { get; set; }
        public string PartKitNo { get; set; }
        public string Description { get; set; }
        public string Representative { get; set; }
        public string QuoteJson { get; set; }
        public string CustomerRequirementJson { get; set; }
        public int ProductId { get; set; }
    }
}
