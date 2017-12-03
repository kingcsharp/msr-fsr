using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Msr.Models.CustomerRequirements;

namespace Msr.Services.Quotes.ViewModels
{
    public class FreeFormQuoteViewModel
    {
        public FreeFormQuoteViewModel()
        {
               QuoteItems = new List<QuoteItemsViewModel>();
        }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string Customer { get; set; }

        public string Contact { get; set; }

        public string FOB { get; set; }

        public string Address { get; set; }

        public string Title { get; set; }

        public string Terms { get; set; }

        public string CityStateZip { get; set; }

        public string Phone { get; set; }

        public string Delivery { get; set; }

        [Required]
        public string ExistingProcess { get; set; }

        public string ProcessDescription { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string TitleSubmit { get; set; }

        [Required]
        public string AddressSubmit { get; set; }

        public List<QuoteItemsViewModel> QuoteItems { get; set; }

        public void Setup()
        {
            if (!QuoteItems.Any())
            {
                QuoteItems.Add(new QuoteItemsViewModel());
            }
        }

    }
}
