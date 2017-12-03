using System.ComponentModel.DataAnnotations;

namespace Msr.Services.Quotes.ViewModels
{
    public class QuoteItemsViewModel
    {
        public int ItemNo { get; set; }

        [Required]
        public int? Quantity { get; set; }
        
        public string Description { get; set; }

        [Required]
        public decimal? LeadTime { get; set; }

        [Required]
        public string CustomerPartNo { get; set; }
        
        [Required]
        public decimal? Price { get; set; }

        public decimal? Extension { get; set; }
    }
}
