using System;
using System.Web.Script.Serialization;
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
                        SubmittedBy = model.SubmittedBy,
                        Status = CustomerSubmittedRequirementConstants.Received,
                        SubmittedDate = DateTime.Now,
                        CustomerRequirementJson = new JavaScriptSerializer().Serialize(model)
                    };

                    _dbContext.CustomerSubmittedRequirements.Add(entity);
                }

                _dbContext.SaveChanges();

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
