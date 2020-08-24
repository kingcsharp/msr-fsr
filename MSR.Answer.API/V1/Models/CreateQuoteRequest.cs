using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateQuoteRequest
    {
        [Required]
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

        [Required]
        public ICollection<CreateQuoteItemRequest> QuoteItems { get; set; }
    }
}
