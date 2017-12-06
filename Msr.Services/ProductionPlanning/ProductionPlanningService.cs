using Msr.Models.ProductionPlanning;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Msr.Models.CustomerRequirements;
using Msr.Services.Procedures;
using Msr.Services.Procedures.Messages;
using Msr.Services.ProductionPlanning.ViewModels;
using Msr.Models.Common;
using Msr.Models.Locations;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.Workflows;

namespace Msr.Services.ProductionPlanning
{
    public class ProductionPlanningService
    {
        private readonly MsrDbContext _dbContext;
        private ProceduresService _proceduresService;
        private readonly WorkflowService _workflowService;

        public ProductionPlanningService()
        {
            _dbContext = new MsrDbContext();
            _proceduresService = new ProceduresService();
            _workflowService = new WorkflowService();
        }

        public IQueryable<CustomerSubmittedRequirement> GetProductionPlaningQueryable()
        {
            return _dbContext.CustomerSubmittedRequirements;
        }
        public IQueryable<PartInfoView> GetProductionPartInfoViewQueryable()
        {
            return _dbContext.PartInfoViews;
        }

        public PartInfoView GetProductionById(int Id)
        {
            return GetProductionPartInfoViewQueryable().SingleOrDefault(x => x.Id == Id);
        }
        public CustomerRequirement GetCustomerById(string Id)
        {
            return _dbContext.CustomerRequirements.SingleOrDefault(x => x.Id == Id);
        }
        public List<RequirementStep> GetStepsByObjectId(int id)
        {
            return _dbContext.RequirementSteps.Where(x => x.CustomerSubmittedRequirementId == id).OrderBy(x => x.Id).ToList();
        }
      
        public IQueryable<LocationView> GetLocationsQueryable()
        {
            return _dbContext.LocationViews;
        }
        public GetStepDataResult GetSteps(string id)
        {
            var procedureName = _proceduresService.GetProceduresQueryable().Where(x => x.ObjectId == id).SingleOrDefault().Name;

            var viewModel = new GetStepDataResult
            {
                GetStepDataResults = _proceduresService.GetStepsData(id, "1618")
            };

            viewModel.AddMonitorForProcedureViewModel.Setup();
            viewModel.AddMonitorForProcedureViewModel.Related_Object_Id = id;
            //ViewBag.ProcObjectId = id;
            //ViewBag.ProdecureName = procedureName;
            return viewModel;
        }

        public string GetPartKitNo(string id)
        {
            var result = _dbContext.CustomerRequirementViews.Where(x => x.Id == id).SingleOrDefault();

            if (result != null)
            {
                return result.PartKitNo;
            }
            else
            {
                return "";
            }
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

        public void UpdateStatus(string id, string type)
        {
            var step = _dbContext.CustomerRequirementViews.Where(x => x.Id == id).SingleOrDefault();

            if (type == "Save & Submit")
            {
                step.Status = "APPROVED";
            }
            else
            {
                step.Status = "IN_APPROVAL";
            }

            _dbContext.SaveChanges();
        }

        public ResultNotification<string> Save(RequirementStepsViewModel model, string submit)
        {
            var result = new ResultNotification<string>();

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

                if (!string.IsNullOrWhiteSpace(submit))
                {
                    var procedureRefId = string.Empty;
                    var procedureSteps = _proceduresService.GetStepsData(model.ProductProcedureId, model.LoginId);

                    foreach (var step in model.Steps)
                    {
                        var existingStep = procedureSteps.SingleOrDefault(x => x.Id == step.ObjectId && x.Print_Order == step.Step);

                        if (existingStep == null)
                        {
                            ////if (string.IsNullOrWhiteSpace(procedureRefId))
                            ////{
                            ////    procedureRefId = _dbContext.Database.SqlQuery<string>($"SELECT HISTORY_REF_ID  FROM A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN WHERE object_id ={model.ProductProcedureId}").Single();
                            ////}

                            var vm = new GetStepEditDataViewModel();
                            vm.GetStepEditData.Step_Text = "<h4>" + procedureSteps.Where(x => x.Id == step.ObjectId).FirstOrDefault().StepTitle + "</h4>";
                            vm.GetStepEditData.Print_Order = step.Step;
                            vm.ProcObjId = model.ProductProcedureId;
                            vm.ReplacementCost = step.ReplacementCost;
                            vm.Utilization = step.Utilization;
                            vm.UsefulLife = step.UsefulLife;
                            vm.EquipExpensePerMinute = step.EquipExpensePerMinute;
                            vm.AnnualRM = step.AnnualRM;
                            vm.RMPerMinute = step.RMPerMinute;

                            _proceduresService.CreateStepData(vm);
                        }
                    }

                    try
                    {
                        var p = new DynamicParameters();

                        p.Add("@newID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                        p.Add("@messages", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                        p.Add("@objID", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@PARENT_ID", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@SUPPLIER_ID", model.ProductSupplierId, DbType.String, ParameterDirection.Input,
                            size: 50);
                        p.Add("@Name", requirment.ProductName, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@COMMENTS", "", DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@PROCEDURE_ID", requirment.ProcedureId, DbType.String, ParameterDirection.Input,
                            size: 50);
                        p.Add("@APP_OBJECT", requirment.PartId, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@CUSTOMIZABLE", "0", DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@REQ_FORM", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@MGR_TEAM", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@SALES_TAX", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@OBJ_USED_ON", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@MGR_ROLE", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@OEM", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@MODEL", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@PROCESS_AREA", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@COPPER", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@MM", null, DbType.String, ParameterDirection.Input, size: 50);
                        p.Add("@strNTLogin", model.LoginId, DbType.String, ParameterDirection.Input, size: 50);


                        using (IDbConnection conn =
                            new SqlConnection(ConfigurationManager.ConnectionStrings["MsrPortal"].ConnectionString))
                        {
                            int i = conn.Execute("A_SP_PRODUCT_UPDATE_ONE_PRODUCT", p,
                                commandType: CommandType.StoredProcedure);

                            var newId = p.Get<string>("newID");
                            var messages = p.Get<string>("messages");

                            //_dbContext.Database.SqlQuery<SelectFile>($"DELETE FROM A_PRODUCTS_QUICK_PRICE WHERE PROD_HIST_ID = (SELECT ID FROM A_PRODUCTS_HISTORY WHERE    OBJECT_ID IN(SELECT ID FROM A_PRODUCTS_HISTORY WHERE OBJECT_ID = '{newId}'))").ToList();

                            //_dbContext.Database.SqlQuery<SelectFile>($"INSERT INTO A_PRODUCTS_QUICK_PRICE (ID,PROD_HIST_ID,CUST_ID,PRICE,DRCM,MODBY,CREATE_PRICE_LIST,PROD_TIME,PROD_TIME_UNIT,CAPACITY,CAPACITY_UNITS) " +
                            //    $"VALUES(newID(), '{newId}', '{requirment.SupplierId}', NULL, getDate(), '{model.LoginId}', '1', '5', 'TIME_SYS_DAYS', NULL, 'minute'))").ToList();

                            var workflow = _workflowService.CheckOutObject(newId, model.LoginId);

                            var p1 = new DynamicParameters();

                            p1.Add("@msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                            p1.Add("@msg2", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                            p1.Add("@objID", workflow.Entity, DbType.String, ParameterDirection.Input, size: 50);
                            p1.Add("@wfID", "37", DbType.String, ParameterDirection.Input,
                                size: 50); //Admin WorkflowAdmin Workflow
                            p1.Add("@revComment", requirment.ProductName, DbType.String, ParameterDirection.Input,
                                size: 2000);
                            p1.Add("@statOnCompletion", "APPROVED", DbType.String, ParameterDirection.Input, size: 50);
                            p1.Add("@allRevs", null, DbType.String, ParameterDirection.Input, size: 50);
                            p1.Add("@strNTLogin", model.LoginId, DbType.String, ParameterDirection.Input, size: 50);

                            conn.Execute("A_SP_OBJECT_START_WF", p1, commandType: CommandType.StoredProcedure);

                            var msg = p1.Get<string>("msg");
                            var msg2 = p1.Get<string>("msg2");

                            result.SuccessMessage = msg;
                        }

                        requirment.Status = CustomerSubmittedRequirementConstants.Completed;
                    }
                    catch (Exception ex)
                    {
                        result.AddError("There was an error creating product");
                    }
                }
                else
                {
                    requirment.Status = CustomerSubmittedRequirementConstants.InProgress;
                }


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

                //UpdateStatus(id, model.postType); need to know how to implement this.

                if (model.Steps.Count > 1)
                {
                    result.SuccessMessage = "Requirement steps have been saved successfully.";
                }
                else
                {
                    result.SuccessMessage = "Requirement step has been saved successfully.";
                }

            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;
                result.AddError(message);
                return result;
            }

            return result;
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

        public ResultNotification<string> ChangeStatus(string id, string status)
        {
            var result = new ResultNotification<string>();
            try
            {
                var step = _dbContext.CustomerRequirementViews.Where(x => x.Id == id).SingleOrDefault();

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

        public List<SelectFile> GetProceduretList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT SHOWNAME as Show,Object_Id as Value   FROM A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN WHERE ( CREATING_CO = '2'  ) AND (( NAME LIKE '%f%' AND NAME LIKE '%%' ) )    ORDER BY SHOWNAME").ToList();

            return result;
        }

        public List<SelectFile> GetPartList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT NAME_COMBO as Show,ID as Value   FROM A_V_PARTS_APPROVED_DATA WHERE ( COMPANY = '2'  ) AND (( NAME_COMBO LIKE '%f%' AND NAME_COMBO LIKE '%%' ) )    ORDER BY NAME_COMBO").ToList();

            return result;
        }

        public List<SelectFile> GetSupplierList()
        {
            var result = _dbContext.Database.SqlQuery<SelectFile>("SELECT DISTINCT NAME as Show,ID as Value FROM A_V_COMPANIES_DROP_SEARCH WHERE ( ROOT_CO_ID = '2' ) AND (( NAME LIKE '%f%' AND NAME LIKE '%%' ) ) ORDER BY NAME").ToList();

            return result;
        }


    }
}
