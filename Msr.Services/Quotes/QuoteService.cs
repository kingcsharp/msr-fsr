using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;
using Dapper;
using Msr.Services.Quotes.ViewModels;
using Msr.Models.CustomerRequirements;
using Msr.Repositories;
using Msr.Services.Users.Messages;

namespace Msr.Services.Quotes
{
    public class QuoteService
    {
        private readonly MsrDbContext _dbContext;

        public QuoteService()
        {
            _dbContext = new MsrDbContext();
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

                    model.ExistingProcess = model.ExistingProcess.Trim();

                    var procedure = _dbContext.Procedures.FirstOrDefault(x => x.Root == model.ExistingProcess && x.Status == "APPROVED");

                    if (procedure != null)
                    {
                        entity.ProcedureId = procedure.Root;
                    }

                    _dbContext.CustomerSubmittedRequirements.Add(entity);
                    _dbContext.SaveChanges();

                    var saveProduct = new SaveProductRequest
                    {
                        ProductName = entity.Description,
                        CustomerId = entity.Company,
                        SupplierId = entity.SupplierId,
                        PartId = entity.PartId,
                        NtLogin = currentUser.Id,
                        LeadTime = entity.LeadTime,
                        Price = entity.Price,
                        CustomerRequirementId = entity.Id,
                        Division = entity.Division,
                        LocationId = entity.LocationId,
                        CycleTime = model.CycleTime
                    };

                    SaveProduct(saveProduct);
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

        public CustomerRequirementView GetById(int id)
        {
            var requirment = _dbContext.CustomerRequirementViews.SingleOrDefault(x => x.CustomerSubmitId == id);

            return requirment;
        }

        public CustomerRequirementView GetCustomerRequirementView(int id)
        {
            var requirment = _dbContext.CustomerRequirementViews
                .FirstOrDefault(x => x.ObjectId == id.ToString());

            return requirment;
        }

        public ResultNotification<string> SaveProduct(SaveProductRequest productRequest)
        {
            var result = new ResultNotification<string>();

            try
            {
                var p = new DynamicParameters();

                p.Add("@newID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p.Add("@messages", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p.Add("@productId", !string.IsNullOrWhiteSpace(productRequest.PObjectId) ? productRequest.PObjectId : null, DbType.String, ParameterDirection.Input, 50);
                p.Add("@productName", productRequest.ProductName, DbType.String, ParameterDirection.Input, 50);
                p.Add("@custId", productRequest.CustomerId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@supplierId", productRequest.SupplierId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@partId", productRequest.PartId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@procedureId", productRequest.ProcedureId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@loginId", productRequest.NtLogin, DbType.String, ParameterDirection.Input, 50);
                p.Add("@leadTime", productRequest.LeadTime, DbType.Double, ParameterDirection.Input, 50);
                p.Add("@price", productRequest.Price, DbType.Double, ParameterDirection.Input, 50);
                p.Add("@totalSalePrice", productRequest.Price, DbType.Single, ParameterDirection.Input, 50);
                p.Add("@materialCost", productRequest.MaterialCost, DbType.Single, ParameterDirection.Input, 50);
                p.Add("@customerRequirementId", productRequest.CustomerRequirementId, DbType.Int32, ParameterDirection.Input);
                p.Add("@IsProduct", productRequest.IsProduct, DbType.Boolean, ParameterDirection.Input);
                p.Add("@Division", productRequest.Division, DbType.String, ParameterDirection.Input, 50);
                p.Add("@LocationId", productRequest.LocationId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@CycleTime", productRequest.CycleTime, DbType.Int32, ParameterDirection.Input);

                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    int i = conn.Execute("Portal_Create_UpdateProduct", p, commandType: CommandType.StoredProcedure);
                    var newId = p.Get<string>("newID");
                    var messages = p.Get<string>("messages");
                }
            }
            catch (Exception)
            {
                result.AddError("There was an error creating product");
            }

            return result;
        }
    }
}
