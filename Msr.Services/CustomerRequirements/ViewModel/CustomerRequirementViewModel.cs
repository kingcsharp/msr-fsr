using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Msr.Models.CustomerRequirement;
using Msr.Models.CustomerRequirements;

namespace Msr.Services.CustomerRequirements.ViewModel
{
    public class CustomerRequirementViewModel
    {        
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
        public string CommercialPhone { get; set; }
        [EmailAddress]
        public string CommercialEmail { get; set; }
        public string TechnicalName { get; set; }
        public string TechnicalTitle { get; set; }
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
        [Required]
        public DateTime? SubmittedDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string SubmittedBy { get; set; }

        public IEnumerable<SelectListItem> ShippingMehtodList { get; set; }
        [NotMapped]
        public List<ProcessInfoView> ProcessInfoViews { get; set; }
        [NotMapped]
        public List<PartInfoView> PartInfoViews { get; set; }

        public string postType { get; set; }

        public void Setup(CustomerRequirementService customerRequirementService)
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

            ProcessInfoViews = customerRequirementService.ProcessInfoView(Id);

            PartInfoViews = customerRequirementService.PartInfoViews(Id);
        }

        public CustomerRequirementViewModel MapToDto(CustomerRequirementView model)
        {
            return new CustomerRequirementViewModel()
            {
                Id = model.Id,
                RequirementName = model.RequirementName,
                Location = model.Location,
                Respresentative = model.Respresentative,
                Company = model.Company,
                DivisionFab = model.DivisionFab,
                StreetAddress = model.StreetAddress,
                CityStateZIP = model.CityStateZIP,
                CommercialName = model.CommercialName,
                CommercialTitle = model.CommercialTitle,
                CommercialPhone = model.CommercialPhone,
                CommercialEmail = model.CommercialEmail,
                TechnicalName = model.TechnicalName,
                TechnicalTitle = model.TechnicalTitle,
                TechnicalEmail = model.TechnicalEmail,
                TechnicalPhone = model.TechnicalPhone,
                ShortDescription = model.ShortDescription,
                PartKitNo = model.PartKitNo,
                CustomerSpecifications = model.CustomerSpecifications,
                InProcessAnalyticalRequirements = model.InProcessAnalyticalRequirements,
                DetailedDescription = model.DetailedDescription,
                CriticalToFunction = model.CriticalToFunction,
                SpecificCustomerQualification = model.SpecificCustomerQualification,
                ForecastedVolumes = model.ForecastedVolumes,
                SpecialPackagingShipping = model.SpecialPackagingShipping,
                ExpectedQuoteDate = model.ExpectedQuoteDate,
                ExpectedCycleTime = model.ExpectedCycleTime,
                PickupNotification = model.PickupNotification,
                ShippingMethod = model.ShippingMethod,
                AdditionalInformation = model.AdditionalInformation,
                SubmittedDate = model.SubmittedDate,
                Status = model.Status,
                SubmittedBy = model.SubmittedBy
            };
        }

    }
}
