using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkExtras.EF6;
using Msr.Models.CustomerRequirement;
using Msr.Models.CustomerRequirements;
using Msr.Repositories;
using Msr.Services.CustomerRequirements.ViewModel;
using Msr.Services.EquipmentMaintenance.ViewModels;
using Msr.Services.EquipmentMaintenances.Procedures;

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
        public CustomerRequirementView GetById(string Id)
        {
            return GetProductionPlaningQueryable().Where(x => x.Id == Id).SingleOrDefault();
        }
        public List<ProcessInfoView> ProcessInfoView(string id)
        {
            return _dbContext.ProcessInfoViews.Where(x => x.ObjectId == id).ToList();
        }
        public List<PartInfoView> PartInfoViews(string id)
        {
            return _dbContext.PartInfoViews.Where(x => x.ObjectId == id).ToList();
        }
        public ResultNotification<string> Create(CustomerRequirementViewModel model, List<ProcessInfoViewModel> process, List<PartInfoViewModel> part)
        {
            var result = new ResultNotification<string>();
            try
            {
                var getIdProcedure = new GetIdProcedure { };

                var res = _dbContext.Database.ExecuteStoredProcedure<GetNewIdModel>(getIdProcedure);

                var customerRequirement = CustomerRequirementMapping(model);

                customerRequirement.Id = getIdProcedure.NewID;

                _dbContext.CustomerRequirements.Add(customerRequirement);

                foreach (var processRecord in process)
                {
                    var processInfo = ProcessInfoMapping(processRecord);

                    processInfo.ObjectId = customerRequirement.Id;

                    _dbContext.ProcessInfoViews.Add(processInfo);
                }
                foreach (var partRecord in part)
                {
                    var partInfo = PartInfoMapping(partRecord);

                    partInfo.ObjectId = customerRequirement.Id;

                    _dbContext.PartInfoViews.Add(partInfo);
                }
                _dbContext.SaveChanges();

                result.SuccessMessage = "Requirement has been saved successfully.";
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                result.AddError(message);               
            }

            return result;
        }
        public PartInfoView PartInfoMapping(PartInfoViewModel model)
        {
            PartInfoView partInfoView = new PartInfoView();

            partInfoView.Id = model.Id;
            partInfoView.ObjectId = model.ObjectId;
            partInfoView.PartDescription = model.PartDescription;
            partInfoView.Substrate = model.Substrate;
            partInfoView.CoatingSurface = model.CoatingSurface;
            partInfoView.CustPartNo = model.CustPartNo;
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
