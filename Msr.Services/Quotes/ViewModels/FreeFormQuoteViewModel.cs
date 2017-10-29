using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Quotes.ViewModels
{
    public class FreeFormQuoteViewModel
    {
        public string Date { get; set; }
        public string Customer { get; set; }
        public string Contact { get; set; }
        public string FOB { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Terms { get; set; }
        public string CityStateZip { get; set; }
        public string Phone { get; set; }
        public string Delivery { get; set; }
        public string ExistingProcess { get; set; }
        public string ProcessDescription { get; set; }
        public string CreatedBy { get; set; }
        public string TitleSubmit { get; set; }
        public string AddressSubmit { get; set; }

        public string Email { get; set; }

        public List<QuoteItemsViewModel> QuoteItemsViewModels { get; set; }
    }
}
