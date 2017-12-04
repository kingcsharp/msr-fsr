using System;
using System.Linq;
using System.Web.Script.Serialization;
using Msr.Services.Quotes.ViewModels;
using Msr.Models.CustomerRequirements;
using Msr.Repositories;

namespace Msr.Services.Quotes
{
    public class QuoteService
    {
        private readonly MsrDbContext _dbContext;

        public QuoteService()
        {
            _dbContext = new MsrDbContext();
        }

        public ResultNotification<string> Create(FreeFormQuoteViewModel model)
        {
            var response = new ResultNotification<string>();

            try
            {
                foreach (var item in model.QuoteItems)
                {
                    var entity = new CustomerSubmittedRequirement
                    {
                        Company = model.Customer,
                        Description = item.Description,
                        SubmittedBy = model.CreatedBy,
                        SubmittedDate = model.Date.Value,
                        Status = CustomerSubmittedRequirementConstants.Received,
                        QuoteJson = new JavaScriptSerializer().Serialize(model)
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

        public CustomerSubmittedRequirement GetById(int id)
        {
            var requirment = _dbContext.CustomerSubmittedRequirements.SingleOrDefault(x => x.Id == id);

            return requirment;
        }
    }
}
