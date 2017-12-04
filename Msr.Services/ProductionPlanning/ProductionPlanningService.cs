using Msr.Models.ProductionPlanning;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Msr.Models.CustomerRequirements;
using Msr.Services.Procedures;
using Msr.Services.Procedures.Messages;
using Msr.Services.ProductionPlanning.ViewModels;
using Msr.Models.Common;
using Msr.Models.Locations;

namespace Msr.Services.ProductionPlanning
{
    public class ProductionPlanningService
    {
        private readonly MsrDbContext _dbContext;
        private ProceduresService _proceduresService;

        public ProductionPlanningService()
        {
            _dbContext = new MsrDbContext();
            _proceduresService = new ProceduresService();
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

        public ResultNotification<string> Save(RequirementStepsViewModel model)
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
