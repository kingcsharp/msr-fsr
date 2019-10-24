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

        [Required]
        public string Title { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter valid phone number.")]
        public int? Phone { get; set; }

        public string Delivery { get; set; }

        public string Email { get; set; }

        public string PhoneCSR { get; set; }

        public string PartKitNo { get; set; }

        [Required]
        public string ExistingProcess { get; set; }

        public string ProcessDescription { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string TitleSubmit { get; set; }

        [Required]
        public string AddressSubmit { get; set; }

        public string SupplierName { get; set; }

        public string ProductId { get; set; }

        public int? CycleTime { get; set; }

        public List<QuoteItemsViewModel> QuoteItems { get; set; }

        public List<SelectListItem> Suppliers { get; set; }

        public List<SelectListItem> Customers { get; set; }
        public string CustomerName { get; set; }

        public void Setup(ProductionPlanningService productionPlanningService, LoggedUserIdResult currentUser)
        {
            if (!QuoteItems.Any())
            {
                QuoteItems.Add(new QuoteItemsViewModel());
            }

            Suppliers = productionPlanningService.GetSupplierList(currentUser).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString(),
                Selected = x.Value == "2" // REQ TO ALWAYS DEFAULT TO MSR-FSR
            }).OrderBy(o => o.Text).ToList();

            Customers = productionPlanningService.GetCustomerList(currentUser).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();
        }
    }
}
