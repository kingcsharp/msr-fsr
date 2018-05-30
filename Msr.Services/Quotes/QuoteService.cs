using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;
using Dapper;
using Msr.Services.Quotes.ViewModels;
using Msr.Models.CustomerRequirements;
using Msr.Models.Parts;
using Msr.Repositories;
using Msr.Services.Parts;
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
                        QuoteJson = new JavaScriptSerializer().Serialize(model),
                        SupplierId = model.Supplier
                    };

                    item.CustomerPartNo = item.CustomerPartNo.Trim();

                    var part = _dbContext.PartsViews.Where(x => x.CompanyPartNumber == item.CustomerPartNo).OrderByDescending(x => x.CreateDate).FirstOrDefault();

                    if (part != null)
                    {
                        entity.PartId = part.ObjectId;
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
                            entity.PartId = checkOutObject.Entity;
                        }
                    }

                    model.ExistingProcess = model.ExistingProcess.Trim();

                    var procedure = _dbContext.Procedures.FirstOrDefault(x => x.Root == model.ExistingProcess && x.Status == "APPROVED");

                    if (procedure != null)
                    {
                        entity.ProcedureId = procedure.Root;
                    }

                    _dbContext.CustomerSubmittedRequirements.Add(entity);
                    _dbContext.SaveChanges();
                    SaveProduct(entity, response, currentUser.Id);
                }

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

        public CustomerRequirementView GetCustomerRequirementView(int id)
        {
            var requirment = _dbContext.CustomerRequirementViews
                .FirstOrDefault(x => x.ObjectId == id.ToString());

            return requirment;
        }

        public void SaveProduct(CustomerSubmittedRequirement requirment, ResultNotification<string> result, string ntLogin)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@newID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p.Add("@messages", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p.Add("@productId", null, DbType.String, ParameterDirection.Input, 50);
                p.Add("@productName", requirment.Description, DbType.String, ParameterDirection.Input, 50);
                p.Add("@custId", requirment.Company, DbType.String, ParameterDirection.Input, 50);
                p.Add("@supplierId", requirment.SupplierId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@partId", requirment.PartId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@procedureId", null, DbType.String, ParameterDirection.Input, 50);
                p.Add("@loginId", ntLogin, DbType.String, ParameterDirection.Input, 50);
                p.Add("@leadTime", requirment.LeadTime, DbType.Double, ParameterDirection.Input, 50);
                p.Add("@price", requirment.Price, DbType.Double, ParameterDirection.Input, 50);
                p.Add("@totalSalePrice", null, DbType.Single, ParameterDirection.Input, 50);
                p.Add("@materialCost", null, DbType.Single, ParameterDirection.Input, 50);
                p.Add("@customerRequirementId", requirment.Id, DbType.Int32, ParameterDirection.Input);
                p.Add("@IsProduct", false, DbType.Boolean, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    int i = conn.Execute("Portal_Create_UpdateProduct", p, commandType: CommandType.StoredProcedure);
                    var newId = p.Get<string>("newID");
                    var messages = p.Get<string>("messages");

                    var requirements = _dbContext.CustomerSubmittedRequirements.SingleOrDefault(x => x.Id == requirment.Id);
                    requirements.ProductId = newId;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.AddError("There was an error creating product");
            }
        }
    }
}
