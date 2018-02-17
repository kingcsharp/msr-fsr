using Msr.Models.ProductionPlanning;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;
using Dapper;
using Msr.Models.CustomerRequirements;
using Msr.Services.Procedures;
using Msr.Services.Procedures.Messages;
using Msr.Services.ProductionPlanning.ViewModels;
using Msr.Models.Common;
using Msr.Models.Locations;
using Msr.Services.CustomerRequirements.ViewModel;
using Msr.Services.PrePro;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.Quotes.ViewModels;
using Msr.Services.Users.Messages;
using Msr.Services.Workflows;
using Msr.Services.Workflows.ViewModels;

namespace Msr.Services.ProductionPlanning
{
    public class ProductionPlanningService
    {
        private readonly MsrDbContext _dbContext;
        private ProceduresService _proceduresService;
        private readonly WorkflowService _workflowService;
        private readonly PreProServices _preProServices;
        private readonly ProceduresService _preProceduresService;

        public ProductionPlanningService()
        {
            _dbContext = new MsrDbContext();
            _proceduresService = new ProceduresService();
            _workflowService = new WorkflowService();
            _preProServices = new PreProServices();
            _preProceduresService = new ProceduresService();
        }

        public IQueryable<CustomerRequirementView> GetProductionPlaningQueryable()
        {
            return _dbContext.CustomerRequirementViews;
        }
        public IQueryable<PartInfoView> GetProductionPartInfoViewQueryable()
        {
            return _dbContext.PartInfoViews;
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
            step.Step = model.Step;
            step.StandardDirectLaborMinutes = model.StandardDirectLaborMinutes;
            step.StandardMachineMinutes = model.StandardMachineMinutes;
            step.ReplacementCost = model.ReplacementCost;
            step.Utilization = model.Utilization;
            step.UsefulLife = model.UsefulLife;
            step.EquipExpensePerMinute = model.EquipExpensePerMinute;
            step.AnnualRm = model.AnnualRm;
            step.RmPerMinute = model.RmPerMinute;
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
                var requirment = _dbContext.CustomerSubmittedRequirements.Single(x => x.Id == model.Id);
                requirment.SupplierId = model.ProductSupplierId;
                requirment.LocationId = model.ProductLocationId;
                requirment.Customer = model.ProductCustomerName;
                requirment.ProductName = model.ProductName;
                requirment.PartId = model.ProductPartId;
                requirment.ProcedureId = model.ProductProcedureId;
                requirment.Division = model.ProductCustomerDivision;

                if (string.IsNullOrWhiteSpace(requirment.ProductId))
                {
                    CreateProduct(model, requirment, result);
                }

                if (string.IsNullOrWhiteSpace(model.ProductProcedureId))
                {
                    var saveProcedureViewModel = new SaveProcedureViewModel { Name = requirment.ProductName, DurationType = "TIME_SYS_SECONDS", SecurityLevel = "1", StepInAp = 1, WipMsg = 0 };

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

                var procedureObjectId = GetProceduretById(requirment.ProcedureId).Value;

                var procedureSteps = _proceduresService.GetStepsData(procedureObjectId, model.LoginId);

                foreach (var step in model.Steps)
                {
                    if (string.IsNullOrWhiteSpace(step.ObjectId))
                    {
                        step.ObjectId = "0";
                    }
                    var existingStep = procedureSteps.SingleOrDefault(x => x.Id == step.ObjectId);

                    if (existingStep == null)
                    {
                        var template = _preProServices.GetById(step.Process);

                        var vm = new GetStepEditDataViewModel
                        {
                            GetStepEditData =
                            {
                                Step_Text = template.StepText,
                                Print_Order = step.Step
                            },
                            ProcObjId = model.ProductProcedureId,
                            ReplacementCost = step.ReplacementCost,
                            Utilization = step.Utilization,
                            UsefulLife = step.UsefulLife,
                            EquipExpensePerMinute = step.EquipExpensePerMinute,
                            AnnualRM = step.AnnualRM,
                            RMPerMinute = step.RMPerMinute
                        };

                        var newStepData = _proceduresService.CreateStepData(vm);
                        step.ObjectId = newStepData.Entity;
                    }
                }


                requirment.Status = !string.IsNullOrWhiteSpace(submit) ? CustomerSubmittedRequirementConstants.Completed : CustomerSubmittedRequirementConstants.InProgress;

                foreach (var step in model.Steps)
                {
                    var requirementStep = StepMapping(step);

                    var record = _dbContext.RequirementSteps.SingleOrDefault(x => x.Id == requirementStep.Id);

                    if (record == null)
                    {
                        requirementStep.CustomerSubmittedRequirementId = model.Id;

                        _dbContext.RequirementSteps.Add(requirementStep);
                    }
                    else
                    {
                        UpdateStep(requirementStep);
                    }
                }

                var stepsIds = model.Steps.Select(x => x.Id).ToList();

                var stepsToDelete = _dbContext.RequirementSteps.Where(x => x.CustomerSubmittedRequirementId == model.Id && !stepsIds.Contains(x.Id)).ToList();

                foreach (var step in stepsToDelete)
                {
                    _dbContext.RequirementSteps.Remove(step);
                }

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

        private void CreateProduct(RequirementStepsViewModel model, CustomerSubmittedRequirement requirment, ResultNotification<CustomerSubmittedRequirement> result)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@newID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                p.Add("@messages", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                p.Add("@productName", model.ProductName, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@supplierId", model.ProductSupplierId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@partId", requirment.PartId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@procedureId", requirment.ProcedureId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@loginId", model.LoginId, DbType.String, ParameterDirection.Input, size: 50);
                p.Add("@leadTime", requirment.LeadTime, DbType.Double, ParameterDirection.Input, size: 50);
                p.Add("@price", requirment.Price, DbType.Double, ParameterDirection.Input, size: 50);


                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                {
                    int i = conn.Execute("Portal_CreateProduct", p, commandType: CommandType.StoredProcedure);
                    var newId = p.Get<string>("newID");
                    var messages = p.Get<string>("messages");

                    requirment.ProductId = newId;
                }
            }
            catch (Exception ex)
            {
                result.AddError("There was an error creating product");
            }
        }

        private void SubmitProductToWorkflow(RequirementStepsViewModel model, ResultNotification<string> result, string newId)
        {
            var workflow = _workflowService.CheckOutObject(newId, model.LoginId);

            var submitWorkflow = new SubmitWorkflowViewModel();
            submitWorkflow.CompletionStart = "APPROVED";
            submitWorkflow.ObjectId = workflow.Entity;
            submitWorkflow.ApprovalWorflowId = "37";
            submitWorkflow.Comment = "Product approved by system";
            submitWorkflow.LoginId = model.LoginId;
            var workflowResponse = _workflowService.SubmitWorkflow(submitWorkflow);

            result.SuccessMessage = workflowResponse.SuccessMessage;
        }

        public RequirementStep StepMapping(RequirementStepsDetailsViewModel model)
        {
            var step = new RequirementStep
            {
                Id = model.Id,
                ObjectId = model.ObjectId,
                Process = model.Process,
                Step = model.Step,
                StandardDirectLaborMinutes = model.StandardDirectLaborMinutes.GetValueOrDefault(),
                StandardMachineMinutes = model.StandardMachineMinutes.GetValueOrDefault(),
                ReplacementCost = model.ReplacementCost,
                Utilization = model.Utilization,
                UsefulLife = model.UsefulLife,
                EquipExpensePerMinute = model.EquipExpensePerMinute,
                AnnualRm = model.AnnualRM,
                RmPerMinute = model.RMPerMinute
            };

            return step;
        }

        public List<RequirementStep> GetStepsByRequirementId(int id)
        {
            var result = _dbContext.RequirementSteps.Where(x => x.CustomerSubmittedRequirementId == id).ToList();

            return result;
        }


        public void DeleteStep(int id)
        {
            var result = _dbContext.RequirementSteps.Single(x => x.Id == id);

            _dbContext.RequirementSteps.Remove(result);
            _dbContext.SaveChanges();
        }

        public ResultNotification<string> ChangeStatus(int id, string status)
        {
            var result = new ResultNotification<string>();
            try
            {
                var step = _dbContext.CustomerRequirementViews.SingleOrDefault(x => x.Id == id);

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
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT SHOWNAME as Show,OBJECT_ID as Value   FROM A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN WHERE ID =" + id + "").SingleOrDefault();

            return result;
        }
        public List<SelectFile> GetProceduretList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT SHOWNAME as Show,ID as Value   FROM A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN  ORDER BY SHOWNAME").ToList();

            return result;
        }
        public List<SelectFile> GetPartList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT NAME_COMBO as Show,ID as Value   FROM A_V_PARTS_APPROVED_DATA ORDER BY NAME_COMBO").ToList();

            return result;
        }

        public List<SelectFile> GetSupplierList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT NAME as Show,ID as Value FROM A_V_COMPANIES_DROP_SEARCH WHERE ( ROOT_CO_ID = '2' )  ORDER BY NAME").ToList();

            return result;
        }
        public string GetSupplierIdByName(string name)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT NAME as Show,ID as Value FROM A_V_COMPANIES_DROP_SEARCH WHERE ( ROOT_CO_ID = '2' ) and (ROOT_NAME='" + name + "') ORDER BY NAME").SingleOrDefault();

            return result?.Value;
        }
        public string GetProceduretIdById(string id)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT SHOWNAME as Show,ID as Value   FROM A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN WHERE ( CREATING_CO = '2'  ) AND (( NAME LIKE '%%' AND NAME LIKE '%%' ) ) and(ID='" + id + "')   ORDER BY SHOWNAME").SingleOrDefault();

            return result?.Value;
        }
        public string GetPartIdByCompanyPartNumber(string companyPartNumber)
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT NAME_COMBO as Show, ID as Value   FROM A_V_PARTS_APPROVED_DATA WHERE(COMPANY = '2') AND((NAME_COMBO LIKE '%%')) and(COMPANY_PART_NUMBER = '" + companyPartNumber + "')    ORDER BY NAME_COMBO").SingleOrDefault();

            return result?.Value;
        }
        public List<string> Getjsondata(int? id)
        {
            var result = _dbContext.Database.SqlQuery<string>("select QuoteJson from Portal_CustomerSubmittedRequirement where Id = '" + id + "'").ToList();
            return result;
        }
    }
}
