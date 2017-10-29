using System;

namespace Msr.Models.CustomerRequirements
{
    public class CustomerRequirementView
    {
        public string Id { get; set; }
        public string RequirementName { get; set; }
        public string Location { get; set; }
        public string Respresentative { get; set; }

        public string Company { get; set; }
        public string DivisionFab { get; set; }
        public string StreetAddress { get; set; }
        public string CityStateZIP { get; set; }
        public string CommercialName { get; set; }
        public string CommercialTitle { get; set; }
        public string CommercialPhone { get; set; }
        public string CommercialEmail { get; set; }
        public string TechnicalName { get; set; }
        public string TechnicalTitle { get; set; }
        public string TechnicalPhone { get; set; }
        public string TechnicalEmail { get; set; }

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
        public string SubmittedBy { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string Status { get; set; }
    }
}
