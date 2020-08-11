using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateQuoteRequest
    {
        [Required]
        public int CustomerId { get; set; }
        public string PartKitNo { get; set; }
        public string Description { get; set; }
        public string Representative { get; set; }
        public string QuoteJson { get; set; }
        public string CustomerRequirementJson { get; set; }
        public int? ProductId { get; set; }
    }
}
