using MSR.Domain.Commanding;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateQuote : Command
    {
        public int CustomerId { get; set; }
        public string Contact { get; set; }
        public string Delivery { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string ProcessName { get; set; }
        public string Description { get; set; }
        public string Representative { get; set; }
        public string RepresentativeTitle { get; set; }
        public string RepresentativeAddress { get; set; }
        public string QuoteJson { get; set; }
        public string CustomerRequirementJson { get; set; }
        public int? ProductId { get; set; }
        public string PartKitNo { get; set; }

        public ICollection<CreateQuoteItem> QuoteItems { get; set; }
    }
}
