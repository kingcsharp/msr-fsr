using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.CustomerRequirements;
using Msr.Services.ProductionPlanning;

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

        public string Address { get; set; }

        [Required]
        public string Title { get; set; }

        public string Terms { get; set; }

        public string CityStateZip { get; set; }

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

        public List<QuoteItemsViewModel> QuoteItems { get; set; }

        public List<SelectListItem> Suppliers { get; set; }

        public List<SelectListItem> Customers { get; set; }

        public void Setup(ProductionPlanningService productionPlanningService)
        {
            if (!QuoteItems.Any())
            {
                QuoteItems.Add(new QuoteItemsViewModel());
            }

            Suppliers = productionPlanningService.GetSupplierList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            Customers = productionPlanningService.GetCustomerList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

        }

    }
}
