using System;
using System.Web.Script.Serialization;
using Msr.Models.CustomerRequirements;
using Msr.Repositories;
using Msr.Services.CustomerRequirements.ViewModel;
using Msr.Services.Quotes;

namespace Msr.Services.CustomerRequirements
{
    public class CustomerRequirementService
    {
        private readonly MsrDbContext _dbContext;
        private readonly QuoteService _quoteService;

        public CustomerRequirementService()
        {
            _dbContext = new MsrDbContext();
            _quoteService = new QuoteService();
        }

        public ResultNotification<string> Create(CustomerRequirementViewModel model, string ntLogin)
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
                        PartKitNo = model.PartKitNo,
                        SubmittedBy = model.SubmittedBy,
                        Status = CustomerSubmittedRequirementConstants.Received,
                        SubmittedDate = DateTime.Now,
                        CustomerRequirementJson = new JavaScriptSerializer().Serialize(model),
                        Description = item.PartDescription
                    };

                    _dbContext.CustomerSubmittedRequirements.Add(entity);
                    _dbContext.SaveChanges();

                    entity.Description = model.RequirementName;
                    _quoteService.SaveProduct(entity, response, ntLogin);
                }

                response.SuccessMessage = "Customer Requirements has been submitted successfully.";
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                response.AddError(message);
            }

            return response;
        }


    }
}
