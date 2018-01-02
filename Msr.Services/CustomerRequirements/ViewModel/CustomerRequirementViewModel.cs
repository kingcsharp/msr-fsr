using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Amazon.Runtime.Internal;

namespace Msr.Services.CustomerRequirements.ViewModel
{
    public class CustomerRequirementViewModel
    {
        public CustomerRequirementViewModel()
        {
            Parts = new List<PartInfoViewModel>();
            Process = new List<ProcessInfoViewModel>();
        }

        public string Id { get; set; }
        [Required]
        [Display(Name = "Requirement Name")]
        public string RequirementName { get; set; }
        public string Location { get; set; }
        [Required]
        public string Respresentative { get; set; }

        public string Company { get; set; }
        public string DivisionFab { get; set; }
        public string StreetAddress { get; set; }
        public string CityStateZIP { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string CommercialName { get; set; }
        public string CommercialTitle { get; set; }
        [Required]
        [Display(Name = "Phone")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public string CommercialPhone { get; set; }
        [EmailAddress]
        public string CommercialEmail { get; set; }
        public string TechnicalName { get; set; }
        public string TechnicalTitle { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public string TechnicalPhone { get; set; }
        [EmailAddress]
        public string TechnicalEmail { get; set; }

        [Required(ErrorMessage = "Short Description field is required under General Information Tab")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        public string PartKitNo { get; set; }
        public string CustomerSpecifications { get; set; }
        public string InProcessAnalyticalRequirements { get; set; }
        public string DetailedDescription { get; set; }
        public string CriticalToFunction { get; set; }
        public string SpecificCustomerQualification { get; set; }
        public string ForecastedVolumes { get; set; }
        public string SpecialPackagingShipping { get; set; }
        public DateTime? ExpectedQuoteDate { get; set; }
        public string ExpectedCycleTime { get; set; }
        public string PickupNotification { get; set; }
        public string ShippingMethod { get; set; }
        public string AdditionalInformation { get; set; }
        public DateTime? SubmittedDate { get; set; }

        public string Status { get; set; }

        public string SubmittedBy { get; set; }

        public IEnumerable<SelectListItem> ShippingMehtodList { get; set; }

        public List<ProcessInfoViewModel> Process { get; set; }

        public List<PartInfoViewModel> Parts { get; set; }

        public void Setup()
        {
            ShippingMehtodList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Please select....",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = @"UPS",
                    Value = "UPS"
                },
                new SelectListItem
                {
                    Text = @"FEDEX",
                    Value = "FEDEX"
                },
                new SelectListItem
                {
                    Text = @"USPS",
                    Value = "USPS"
                },
                new SelectListItem
                {
                    Text = @"Freight",
                    Value = "Freight"
                },
                new SelectListItem
                {
                    Text = "",
                    Value = ""
                }
            };

            if (!Process.Any())
            {
                Process.Add(new ProcessInfoViewModel());
            }

            if (!Parts.Any())
            {
                Parts.Add(new PartInfoViewModel());
            }
        }

    }
}
