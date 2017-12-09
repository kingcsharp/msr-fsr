using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Msr.Models.CustomerRequirement;
using Msr.Models.CustomerRequirements;
using Msr.Repositories;
using Msr.Services.CustomerRequirements.ViewModel;

namespace Msr.Services.CustomerRequirements
{
    public class CustomerRequirementService
    {
        private readonly MsrDbContext _dbContext;

        public CustomerRequirementService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<CustomerRequirementView> GetProductionPlaningQueryable()
        {
            return _dbContext.CustomerRequirementViews;
        }

        public List<ProcessInfoView> ProcessInfoView(string id)
        {
            return _dbContext.ProcessInfoViews.Where(x => x.ObjectId == id).ToList();
        }

        public List<PartInfoView> PartInfoViews(string id)
        {
            return _dbContext.PartInfoViews.Where(x => x.ObjectId == id).ToList();
        }

        public ResultNotification<string> Create(CustomerRequirementViewModel model)
        {
            var response = new ResultNotification<string>();

            try
            {
                foreach (var item in model.Parts)
                {
                    var entity = new CustomerSubmittedRequirement
                    {
                        Company = model.Company,
                        Division = model.DivisionFab,
                        Description = model.ShortDescription,
                        PartKitNo = model.PartKitNo,
                        Respresentative = model.Respresentative,
                        SubmittedBy = model.SubmittedBy,
                        Status = CustomerSubmittedRequirementConstants.Received,
                        SubmittedDate = DateTime.Now,
                        CustomerRequirementJson = new JavaScriptSerializer().Serialize(model)
                    };

                    _dbContext.CustomerSubmittedRequirements.Add(entity);
                }

                _dbContext.SaveChanges();

                response.SuccessMessage = "Quote has been submitted successfully.";
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                response.AddError(message);
            }

            return response;
        }
        public PartInfoView PartInfoMapping(PartInfoViewModel model)
        {
            PartInfoView partInfoView = new PartInfoView();

            partInfoView.Id = model.Id;
            partInfoView.ObjectId = model.ObjectId;
            partInfoView.PartDescription = model.PartDescription;
            partInfoView.Substrate = model.Substrate;
            partInfoView.CoatingSurface = model.CoatingSurface;
          //  partInfoView.CustPartNo = model.CustPartNo;
            partInfoView.MfgPartNo = model.MfgPartNo;
            partInfoView.PartsPerKit = model.PartsPerKit;

            return partInfoView;
        }
        public ProcessInfoView ProcessInfoMapping(ProcessInfoViewModel model)
        {
            ProcessInfoView processInfoView = new ProcessInfoView();
            
            processInfoView.Id = model.Id;
            processInfoView.ObjectId = model.ObjectId;
            processInfoView.Contaminents = model.Contaminents;
            processInfoView.NonCU = model.NonCU;
            processInfoView.Material = model.Material;
            processInfoView.ApproxDimensions = model.ApproxDimensions;
            processInfoView.ExistingProcess = model.ExistingProcess;

            return processInfoView;
        }
        public CustomerRequirement CustomerRequirementMapping(CustomerRequirementViewModel model)
        {
            var customerRequirement = new CustomerRequirement();

            customerRequirement.Id = model.Id;
            customerRequirement.RequirementName = model.RequirementName;
            customerRequirement.Location = model.Location;
            customerRequirement.Respresentative = model.Respresentative;

            customerRequirement.Company = model.Company;
            customerRequirement.DivisionFab = model.DivisionFab;
            customerRequirement.StreetAddress = model.StreetAddress;
            customerRequirement.CityStateZIP = model.CityStateZIP;
            customerRequirement.CommercialName = model.CommercialName;
            customerRequirement.CommercialTitle = model.CommercialTitle;
            customerRequirement.CommercialPhone = model.CommercialPhone;
            customerRequirement.CommercialEmail = model.CommercialEmail;
            customerRequirement.TechnicalName = model.TechnicalName;
            customerRequirement.TechnicalTitle = model.TechnicalTitle;
            customerRequirement.TechnicalPhone = model.TechnicalPhone;
            customerRequirement.TechnicalEmail = model.TechnicalEmail;

            customerRequirement.ShortDescription = model.ShortDescription;
            customerRequirement.PartKitNo = model.PartKitNo;
            customerRequirement.CustomerSpecifications = model.CustomerSpecifications;
            customerRequirement.InProcessAnalyticalRequirements = model.InProcessAnalyticalRequirements;
            customerRequirement.DetailedDescription = model.DetailedDescription;
            customerRequirement.CriticalToFunction = model.CriticalToFunction;
            customerRequirement.SpecificCustomerQualification = model.SpecificCustomerQualification;
            customerRequirement.ForecastedVolumes = model.ForecastedVolumes;
            customerRequirement.SpecialPackagingShipping = model.SpecialPackagingShipping;
            customerRequirement.ExpectedQuoteDate = model.ExpectedQuoteDate;
            customerRequirement.ExpectedCycleTime = model.ExpectedCycleTime;
            customerRequirement.PickupNotification = model.PickupNotification;
            customerRequirement.ShippingMethod = model.ShippingMethod;
            customerRequirement.AdditionalInformation = model.AdditionalInformation;
            customerRequirement.SubmittedBy = model.SubmittedBy;
            customerRequirement.SubmittedDate = model.SubmittedDate;
            customerRequirement.Status = model.Status;

            return customerRequirement;
        }
    }
}
