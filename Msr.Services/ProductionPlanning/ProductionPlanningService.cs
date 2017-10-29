using Msr.Models.ProductionPlanning;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Services.ProductionPlanning.ViewModels;

namespace Msr.Services.ProductionPlanning
{
    public class ProductionPlanningService
    {
        private readonly MsrDbContext _dbContext;

        public ProductionPlanningService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<ProductionPlanningView> GetProductionPlaningQueryable()
        {
            return _dbContext.ProductionPlanningViews;
        }

        public ProductionPlanningView GetById(string Id)
        {
            return GetProductionPlaningQueryable().Where(x => x.Id == Id).SingleOrDefault();
        }

        public List<RequirementStepsView> GetStepsByObjectId(string Id)
        {
            return _dbContext.RequirementStepsViews.Where(x => x.ObjectId == Id).OrderBy(x => x.Id).ToList();
        }

        public string GetPartKitNo(string Id)
        {
            var result = _dbContext.CustomerRequirementViews.Where(x => x.Id == Id).SingleOrDefault();

            if (result != null)
            {
                return result.PartKitNo;
            }
            else
            {
                return "";
            }
        }

        public void UpdateStep(RequirementStepsView model)
        {
            var step = _dbContext.RequirementStepsViews.Where(x => x.Id == model.Id).SingleOrDefault();
            step.Process = model.Process;
            step.Step = model.Step;
            step.StandardDirectLaborMinutes = model.StandardDirectLaborMinutes;
            step.StandardMachineMinutes = model.StandardMachineMinutes;
            step.ReplacementCost = model.ReplacementCost;
            step.Utilization = model.Utilization;
            step.UsefulLife = model.UsefulLife;
            step.EquipExpensePerMinute = model.EquipExpensePerMinute;
            step.AnnualRM = model.AnnualRM;
            step.RMPerMinute = model.RMPerMinute;
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

        public ResultNotification<string> Edit(string id, RequirementStepsViewModel model)
        {
            var count = model.requirementSteps.Count;

            var result = new ResultNotification<string>();
            try
            {
                foreach (var step in model.requirementSteps)
                {
                    var requirementStep = StepMapping(step);

                    requirementStep.ObjectId = id;

                    if (requirementStep.Id == 0)
                    {
                        _dbContext.RequirementStepsViews.Add(requirementStep);
                    }
                    else
                    {
                        UpdateStep(requirementStep);
                    }

                }
                _dbContext.SaveChanges();

                UpdateStatus(id, model.postType);

                if (model.requirementSteps.Count > 1)
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

        public RequirementStepsView StepMapping(RequirementStepsViewModel model)
        {
            RequirementStepsView step = new RequirementStepsView();

            step.Id = model.Id;
            step.ObjectId = model.ObjectId;
            step.Process = model.Process;
            step.Step = model.Step;
            step.StandardDirectLaborMinutes = model.StandardDirectLaborMinutes;
            step.StandardMachineMinutes = model.StandardMachineMinutes;
            step.ReplacementCost = model.ReplacementCost;
            step.Utilization = model.Utilization;
            step.UsefulLife = model.UsefulLife;
            step.EquipExpensePerMinute = model.EquipExpensePerMinute;
            step.AnnualRM = model.AnnualRM;
            step.RMPerMinute = model.RMPerMinute;

            return step;
        }
        public void DeleteStep(int id)
        {

            var result = _dbContext.RequirementStepsViews.Where(x => x.Id == id).SingleOrDefault();
            if (result != null)
            {
                _dbContext.RequirementStepsViews.Remove(result);
                _dbContext.SaveChanges();
            }

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
    }
}
