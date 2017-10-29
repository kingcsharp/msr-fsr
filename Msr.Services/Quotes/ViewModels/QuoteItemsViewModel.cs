using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Quotes.ViewModels
{
    public class QuoteItemsViewModel
    {
        public int ItemNo { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Description { get; set; }
        public string LeadTime { get; set; }
        public string CustomerPartNo { get; set; }
        public decimal Price { get; set; }
        public string Extension { get; set; }
    }
}
