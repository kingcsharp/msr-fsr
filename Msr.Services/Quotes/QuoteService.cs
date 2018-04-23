using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EntityFrameworkExtras.EF6;
using Msr.Services.Quotes.ViewModels;
using Msr.Models.CustomerRequirements;
using Msr.Models.Parts;
using Msr.Repositories;
using Msr.Services.Parts;
using Msr.Services.Parts.Procedures;
using Msr.Services.Parts.ViewModels;
using Msr.Services.Users.Messages;
using Msr.Services.Workflows;
using Msr.Services.Workflows.ViewModels;

namespace Msr.Services.Quotes
{
    public class QuoteService
    {
        private readonly MsrDbContext _dbContext;
        private readonly WorkflowService _workflowService;


        public QuoteService()
        {
            _dbContext = new MsrDbContext();
            _workflowService = new WorkflowService();
        }

        public ResultNotification<string> Create(FreeFormQuoteViewModel model, LoggedUserIdResult currentUser)
        {
            var response = new ResultNotification<string>();

            try
            {
                foreach (var item in model.QuoteItems)
                {
                    var entity = new CustomerSubmittedRequirement
                    {
                        Company = model.CustomerId.Trim(),
                        LeadTime = item.LeadTime,
                        Price = item.Price,
                        Description = item.Description,
                        SubmittedBy = model.CreatedBy,
                        SubmittedDate = model.Date.Value,
                        Status = CustomerSubmittedRequirementConstants.Received,
                        QuoteJson = new JavaScriptSerializer().Serialize(model)
                    };

                    item.CustomerPartNo = item.CustomerPartNo.Trim();

                    var part = _dbContext.PartsViews.Where(x => x.CompanyPartNumber == item.CustomerPartNo).OrderByDescending(x => x.CreateDate).FirstOrDefault();

                    if (part != null)
                    {
                        entity.PartId = part.Id;
                    }
                    else
                    {
                        var partModel = new AddPartViewModel
                        {
                            CompanyPartNumber = item.CustomerPartNo,
                            Name = item.CustomerPartNo,
                            NTLogin = currentUser.Id
                        };


                        var partservice = new PartsService();

                        response = partservice.Create(partModel);

                        if (!response.HasErrors())
                        {
                            var checkOutObject = _workflowService.CheckOutObject(response.Entity, partModel.NTLogin);

                            var submitWorkflow = new SubmitWorkflowViewModel
                            {
                                CompletionStart = "APPROVED",
                                LoggedUserIdResult = currentUser,
                                ObjectId = checkOutObject.Entity,
                                ApprovalWorflowId = "37",
                                Comment = "Part approved by system",
                                LoginId = currentUser.Id
                            };
                            _workflowService.SubmitWorkflow(submitWorkflow);

                        }
                    }

                    model.ExistingProcess = model.ExistingProcess.Trim();

                    var procedure = _dbContext.Procedures.FirstOrDefault(x => x.Root == model.ExistingProcess && x.Status == "APPROVED");

                    if (procedure != null)
                    {
                        entity.ProcedureId = procedure.Root;
                    }

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
        public PartsView GetPartById(string id)
        {
            return _dbContext.PartsViews.Where(x => x.ObjectId == id).SingleOrDefault();
        }
    }
}
