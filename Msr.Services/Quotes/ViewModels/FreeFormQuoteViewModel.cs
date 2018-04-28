using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.ProductionPlanning;
using Msr.Services.Users.Messages;

namespace Msr.Services.Quotes.ViewModels
{
    public class FreeFormQuoteViewModel
    {
        public FreeFormQuoteViewModel()
        {
            QuoteItems = new List<QuoteItemsViewModel>();
            Customers = new List<SelectListItem>();
            Suppliers = new List<SelectListItem>();
        }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string Supplier { get; set; }

        public string Contact { get; set; }

        public string FOB { get; set; }

        [Required]
        public string Title { get; set; }

        public string Terms { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter valid phone number.")]
        public int? Phone { get; set; }

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

        public string CustomerName { get; set; }

        public string SupplierName { get; set; }

        public List<QuoteItemsViewModel> QuoteItems { get; set; }

        public List<SelectListItem> Suppliers { get; set; }

        public List<SelectListItem> Customers { get; set; }

        public void Setup(ProductionPlanningService productionPlanningService, LoggedUserIdResult currentUser)
        {
            if (!QuoteItems.Any())
            {
                QuoteItems.Add(new QuoteItemsViewModel());
            }

            Suppliers = productionPlanningService.GetSupplierList(currentUser).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            Customers = productionPlanningService.GetCustomerList(currentUser).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();
        }
    }
}
