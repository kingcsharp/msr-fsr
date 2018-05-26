using Msr.Models.ProductionPlanning;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using EntityFrameworkExtras.EF6;
using ExcelDataReader;
using Msr.Models.AdminCostSettings;
using Msr.Models.CustomerRequirements;
using Msr.Services.Procedures;
using Msr.Services.ProductionPlanning.ViewModels;
using Msr.Models.Common;
using Msr.Models.Locations;
using Msr.Services.AdminCostSettings;
using Msr.Services.PrePro;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.ProductionPlanning.Procedures;
using Msr.Services.Quotes;
using Msr.Services.Users.Messages;
using Msr.Services.Workflows;
using Msr.Services.Workflows.ViewModels;

namespace Msr.Services.ProductionPlanning
{
    public class ProductionPlanningService
    {
        private readonly MsrDbContext _dbContext;
        private readonly ProceduresService _proceduresService;
        private readonly WorkflowService _workflowService;
        private readonly PreProServices _preProServices;
        private readonly ProceduresService _preProceduresService;
        private readonly QuoteService _quoteService;
        private readonly AdminCostSettingService _adminCostSettingService;

        public ProductionPlanningService()
        {
            _dbContext = new MsrDbContext();
            _proceduresService = new ProceduresService();
            _workflowService = new WorkflowService();
            _preProServices = new PreProServices();
            _preProceduresService = new ProceduresService();
            _quoteService = new QuoteService();
            _adminCostSettingService = new AdminCostSettingService();
        }

        public IQueryable<CustomerRequirementView> GetProductionPlaningQueryable()
        {
            return _dbContext.CustomerRequirementViews;
        }

        public List<RequirementStep> GetStepsByObjectId(int id)
        {
            return _dbContext.RequirementSteps.Where(x => x.CustomerSubmittedRequirementId == id).OrderBy(x => x.Id).ToList();
        }

        public IQueryable<LocationView> GetLocationsQueryable()
        {
            return _dbContext.LocationViews;
        }

        public void UpdateStep(RequirementStep model)
        {
            var step = _dbContext.RequirementSteps.SingleOrDefault(x => x.Id == model.Id);
            step.Process = model.Process;
            step.ObjectId = model.ObjectId;
            step.Step = model.Step;
            _dbContext.SaveChanges();
        }

        public void UpdateStatus(int id, string status)
        {
            var requirment = _dbContext.CustomerSubmittedRequirements.Single(x => x.Id == id);

            requirment.Status = status;

            _dbContext.SaveChanges();
        }

        public ResultNotification<CustomerSubmittedRequirement> Save(RequirementStepsViewModel model, string submit, LoggedUserIdResult currentUser)
        {
            var result = new ResultNotification<CustomerSubmittedRequirement>();

            try
            {
                var requirmentView = _quoteService.GetCustomerRequirementView(model.Id);

                var requirment = _dbContext.CustomerSubmittedRequirements.SingleOrDefault(x => x.Id == requirmentView.CustomerSubmitId);

                requirment.SupplierId = model.ProductSupplierId;
                requirment.LocationId = model.ProductLocationId;
                requirment.CustomerId = model.ProductCustomerId;
                requirment.ProductName = model.ProductName;
                requirment.PartId = model.ProductPartId;
                requirment.ProcedureId = model.ProductProcedureId;
                requirment.Division = model.ProductCustomerDivision;

                if (string.IsNullOrWhiteSpace(model.ProductProcedureId))
                {
                    var saveProcedureViewModel = new SaveProcedureViewModel
                    {
                        Name = requirment.ProductName,
                        DurationType = "TIME_SYS_SECONDS",
                        SecurityLevel = "1",
                        StepInAp = 1,
                        WipMsg = 0,
                        NTLogin = model.LoginId
                    };

                    var response = _preProceduresService.Create(saveProcedureViewModel);

                    if (!response.HasErrors())
                    {
                        var checkOutObject = _workflowService.CheckOutObject(response.Entity, model.LoginId);

                        var submitWorkflow = new SubmitWorkflowViewModel();
                        submitWorkflow.CompletionStart = "APPROVED";
                        submitWorkflow.LoggedUserIdResult = currentUser;
                        submitWorkflow.ObjectId = checkOutObject.Entity;
                        submitWorkflow.ApprovalWorflowId = "37";
                        submitWorkflow.Comment = "Procedure approved by system";
                        submitWorkflow.LoginId = model.LoginId;
                        _workflowService.SubmitWorkflow(submitWorkflow);

                        requirment.ProcedureId = response.Entity;
                        model.ProductProcedureId = response.Entity;
                    }
                }

                SaveProduct(model, requirment, result);

                var procedureObjectId = GetProceduretById(requirment.ProcedureId).Value;

                var procedureSteps = _proceduresService.GetStepsData(procedureObjectId, model.LoginId);

                ResultNotification<string> checkOutProcedure = null;

                foreach (var step in model.Steps)
                {
                    var existingStep = procedureSteps.SingleOrDefault(x => x.Id == step.ObjectId);

                    if (step.ObjectId == null && existingStep == null)
                    {
                        checkOutProcedure = _workflowService.CheckOutObject(procedureObjectId, model.LoginId);

                        var template = _preProServices.GetById(step.Process);

                        var vm = new GetStepEditDataViewModel
                        {
                            GetStepEditData =
                                {
                                    Step_Text = template.StepText,
                                    Title = template.Title,
                                    Print_Order = step.Step,
                                    EquipmentTime = step.StandardMachineMinutes,
                                    Duration = step.StandardDirectLaborMinutes
                                },
                            ProcObjId = checkOutProcedure.Entity,
                            
                            ReplacementCost = step.ReplacementCost,
                            Utilization = step.Utilization,
                            UsefulLife = step.UsefulLife
                        };

                        var newStepData = _proceduresService.CreateStepData(vm);

                        step.Process = template.Title;
                        step.ObjectId = newStepData.Entity;
                    }
                }

                if (checkOutProcedure != null)
                {
                    var submitWorkflowNew = new SubmitWorkflowViewModel
                    {
                        CompletionStart = "APPROVED",
                        LoggedUserIdResult = currentUser,
                        ObjectId = checkOutProcedure.Entity,
                        ApprovalWorflowId = "37",
                        Comment = "Procedure step approved by system",
                        LoginId = model.LoginId
                    };
                    _workflowService.SubmitWorkflow(submitWorkflowNew);
                }


                requirment.Status = !string.IsNullOrWhiteSpace(submit) ? CustomerSubmittedRequirementConstants.Completed : CustomerSubmittedRequirementConstants.InProgress;

                _dbContext.SaveChanges();

                result.Entity = requirment;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                result.AddError(message);
                return result;
            }

            return result;
        }

        private void SaveProduct(RequirementStepsViewModel model, CustomerSubmittedRequirement requirment, ResultNotification<CustomerSubmittedRequirement> result)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@newID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p.Add("@messages", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p.Add("@productId", requirment.ProductId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@productName", model.ProductName, DbType.String, ParameterDirection.Input, 50);
                p.Add("@custId", model.ProductCustomerId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@supplierId", model.ProductSupplierId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@partId", requirment.PartId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@procedureId", requirment.ProcedureId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@loginId", model.LoginId, DbType.String, ParameterDirection.Input, 50);
                p.Add("@leadTime", requirment.LeadTime, DbType.Double, ParameterDirection.Input, 50);
                p.Add("@price", requirment.Price, DbType.Double, ParameterDirection.Input, 50);
                p.Add("@totalSalePrice", model.TotalSalePrice, DbType.Single, ParameterDirection.Input, 50);
                p.Add("@materialCost", model.MaterialCost, DbType.Single, ParameterDirection.Input, 50);


                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    int i = conn.Execute("Portal_Create_UpdateProduct", p, commandType: CommandType.StoredProcedure);
                    var newId = p.Get<string>("newID");
                    var messages = p.Get<string>("messages");

                    requirment.ProductId = newId;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.AddError("There was an error creating product");
            }
        }

        public RequirementStep StepMapping(RequirementStepsDetailsViewModel model, AdminCostSetting adminCost)
        {
            var step = new RequirementStep
            {
                Id = model.Id,
                ObjectId = model.ObjectId,
                Process = model.Process,
                Step = model.Step
            };

            return step;
        }

        public ResultNotification<string> ChangeStatus(int id, string status)
        {
            var result = new ResultNotification<string>();
            try
            {
                var step = _dbContext.CustomerRequirementViews.SingleOrDefault(x => x.CustomerSubmitId == id);

                step.Status = status;

                _dbContext.SaveChanges();

                result.SuccessMessage = "Status has been updated successfully !";
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                result.AddError(message);

                return result;
            }

            return result;

        }
        public SelectFile GetProceduretById(string id)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>($"SELECT DISTINCT SHOWNAME as Show,OBJECT_ID as Value   FROM A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN WHERE ID ='{id}'").SingleOrDefault();

            return result;
        }
        public List<SelectFile> GetProceduretList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT SHOWNAME as Show,ID as Value   FROM A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN WHERE SHOWNAME IS NOT NULL ORDER BY SHOWNAME").ToList();

            return result;
        }
        public List<SelectFile> GetPartList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT NAME_COMBO as Show,ID as Value   FROM A_V_PARTS_APPROVED_DATA  WHERE NAME_COMBO IS NOT NULL ORDER BY NAME_COMBO").ToList();

            return result;
        }

        public List<ListItem> GetSupplierList(LoggedUserIdResult currentUser)
        {
            var sql = $"SELECT DISTINCT NAME , ID AS Value FROM A_V_COMPANIES_DROP_SEARCH WHERE ( ROOT_CO_ID = '{currentUser.Root_Company}' ) ORDER BY NAME";

            var result = _dbContext.Database.SqlQuery<ListItem>(sql).ToList();

            return result;
        }

        public List<ListItem> GetCustomerList(LoggedUserIdResult currentUser)
        {
            var sql = $"SELECT DISTINCT TOP 500 NAME, ID AS Value FROM A_V_COMPANIES_DROP_SEARCH WHERE ( ROOT_CO_ID = ID OR ROOT_CO_ID = '{currentUser.Root_Company}' ) ORDER BY NAME";

            var result = _dbContext.Database.SqlQuery<ListItem>(sql).ToList();

            return result;
        }

        public string GetProceduretIdById(string id)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT SHOWNAME as Show,ID as Value   FROM A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN WHERE ( CREATING_CO = '2'  ) AND (( NAME LIKE '%%' AND NAME LIKE '%%' ) ) and(ID='" + id + "')   ORDER BY SHOWNAME").SingleOrDefault();

            return result?.Value;
        }

        public string GetPartIdByCompanyPartNumber(string companyPartNumber)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT NAME_COMBO as Show, ID as Value   FROM A_V_PARTS_APPROVED_DATA WHERE(COMPANY = '2') AND((NAME_COMBO LIKE '%%')) and(COMPANY_PART_NUMBER = '" + companyPartNumber + "')    ORDER BY NAME_COMBO").FirstOrDefault();

            return result?.Value;
        }

        public ResultNotification<List<ProductImportViewModel>> ImportProducts(HttpPostedFileBase postedFile, LoggedUserIdResult ntLogin)
        {
            var result = new ResultNotification<List<ProductImportViewModel>>
            {
                Entity = new List<ProductImportViewModel>()
            };

            try
            {
                if (!postedFile.FileName.EndsWith(".csv"))
                {
                    result.AddError("The import file must be a tab delimited text file");
                    return result;
                }

                var reader = ExcelReaderFactory.CreateCsvReader(postedFile.InputStream);

                var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                reader.Close();

                var columnNames = (from dc in ds.Tables[0].Columns.Cast<DataColumn>()
                                   select dc.ColumnName).ToList();

                var primes = ProductImportViewModel.GetHeaderColumns();

                var results = primes.Where(m => !columnNames.Contains(m));
                var isSubset = primes.Intersect(columnNames).Count() == primes.Count();

                if (!isSubset)
                {
                    result.AddError("Coloums missing : (" + string.Join(",", results) + ") to create Product");
                    return result;
                }

                var modelList = Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new ProductImportViewModel
                {
                    ExternalCustId = item["ExternalCustId"].ToString(),
                    ExternalProcedureId = item["ExternalProcedureId"].ToString(),
                    ExternalPartId = item["ExternalPartId"].ToString(),
                    ExternalProductId = item["ExternalProductId"].ToString(),
                    ProductName = item["ProductName"].ToString(),
                    ExternalProductSupplierId = item["ExternalProductSupplierId"].ToString(),
                    ExternalAccountSupplierId = item["ExternalAccountSupplierId"].ToString(),
                    InternalCustomerId = item["InternalCustomerId"].ToString(),
                    InternalProductSupplierId = item["InternalProductSupplierId"].ToString(),
                    InternalAccountSupplierId = item["InternalAccountSupplierId"].ToString(),
                    InternalRoleId = item["InternalRoleId"].ToString(),
                    InternalPartId = item["InternalPartId"].ToString(),
                    Oem = item["Oem"].ToString(),
                    Model = item["Model"].ToString(),
                    Area = item["Area"].ToString(),
                    Cu = item["Cu"].ToString(),
                    Mm = item["Mm"].ToString(),
                    Price = item["Price"].ToString(),
                    ResponseTime = item["ResponseTime"].ToString(),
                    SalesTax = item["SalesTax"].ToString(),
                    InternalProcedureId = item["InternalProcedureId"].ToString(),
                    IsKit = item["IsKit"].ToString(),
                    KitId = item["KitId"].ToString(),
                    KitQty = item["KitQty"].ToString()
                }).ToList();

                ProcessRows(ntLogin, modelList, result);

                return result;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return result;
            }
        }

        private void ProcessRows(LoggedUserIdResult ntLogin, List<ProductImportViewModel> modelList, ResultNotification<List<ProductImportViewModel>> result)
        {
            foreach (var model in modelList)
            {
                if (string.IsNullOrWhiteSpace(model.ExternalProductId))
                {
                    model.Messages.Add($"{nameof(ProductImportViewModel.ExternalProductId)} is required");
                }

                if (string.IsNullOrWhiteSpace(model.ExternalCustId))
                {
                    model.Messages.Add($"{nameof(ProductImportViewModel.ExternalCustId)} is required");
                }

                if (string.IsNullOrWhiteSpace(model.ExternalPartId))
                {
                    model.Messages.Add($"{nameof(ProductImportViewModel.ExternalPartId)} is required");
                }

                if (string.IsNullOrWhiteSpace(model.ExternalProcedureId))
                {
                    model.Messages.Add($"{nameof(ProductImportViewModel.ExternalProcedureId)} is required");
                }

                if (model.Messages.Any())
                {
                    result.Entity.Add(model);
                    continue;
                }

                model.LoginId = ntLogin.Id;
                var productImportUpdateExternalProcedure = new ProductImportUpdateExternalProcedure(model);

                _dbContext.Database.ExecuteStoredProcedure(productImportUpdateExternalProcedure);

                if (!productImportUpdateExternalProcedure.NewId.Contains("ERROR"))
                {
                    model.Processed = true;
                }

                model.Messages.Add(productImportUpdateExternalProcedure.NewId);

                result.Entity.Add(model);
            }
        }
    }
}
