using System.ComponentModel.DataAnnotations;

namespace Msr.Services.Quotes.ViewModels
{
    public class QuoteItemsViewModel
    {
        public int ItemNo { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double? LeadTime { get; set; }

        [Required]
        public string CustomerPartNo { get; set; }
        
        [Required]
        public double? Price { get; set; }

        public decimal? Extension { get; set; }
    }
}
