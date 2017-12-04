using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Msr.Services.Parts.ViewModels;
using Msr.Models.CustomerRequirements;
using Msr.Services.Procedures.ViewModels;
using Msr.Services.Procedures.Messages;

namespace Msr.Services.ProductionPlanning.ViewModels
{
    public class RequirementStepsViewModel
    {
        public RequirementStepsViewModel()
        {
            AddPartViewModel = new AddPartViewModel();
            SaveProcedureViewModel = new SaveProcedureViewModel();
            Steps = new List<RequirementStepsDetailsViewModel>();
        }

        public CustomerSubmittedRequirement SubmittedRequirement { get; set; }
        public bool HasQuote { get; set; }

        public int Id { get; set; }
        public string ObjectId { get; set; }
        public string OldProductProcedureId { get; set; }
        [Required]
        public string ProductProcedureId { get; set; }
        public string ProductPartId { get; set; }
        public string ProductName { get; set; }
        public string ProductSupplierId { get; set; }
        public string ProductLocationId { get; set; }
        public string ProductCustomerName { get; set; }
        public string ProductCustomerDivision { get; set; }

        [Display(Name = "Total Direct Mins")]

        public decimal TotalDirectMins { get; set; }

        [Display(Name = "Total Machine Mins")]

        public decimal TotalMachineMins { get; set; }

        [Display(Name = "Total Direct Dollar")]

        public decimal TotalDirectDollar { get; set; }

        [Display(Name = "Standard Machine Dollar")]

        public decimal StandardMachineDollar { get; set; }

        [Display(Name = "Total Sale Price")]

        public decimal TotalSalePrice { get; set; }

        public AddPartViewModel AddPartViewModel { get; set; }
        public SaveProcedureViewModel SaveProcedureViewModel { get; set; }
        public List<RequirementStepsDetailsViewModel> Steps { get; set; }
        public List<SelectListItem> ProcessList { get; set; }
        public List<SelectListItem> ProductSupplierList { get; set; }
        public List<SelectListItem> ProductLocationList { get; set; }
        public List<SelectListItem> ProcedureList { get; set; }
        public List<SelectListItem> PartList { get; set; }

        public void Setup(ProductionPlanningService productionPlanningService, List<GetStepDataResult> stepsdropdownDataResults)
        {
            ProcedureList = productionPlanningService.GetProceduretList().Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            PartList = productionPlanningService.GetPartList().Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            ProductSupplierList = productionPlanningService.GetSupplierList().Select(x => new SelectListItem
            {
                Text = x.Show,
                Value = x.Value.ToString()
            }).OrderBy(o => o.Text).ToList();

            ProductLocationList = productionPlanningService.GetLocationsQueryable().Where(x => x.Status == "APPROVED").Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ObjId
            }).OrderBy(o => o.Text).ToList();

            ProcessList = stepsdropdownDataResults.Select(x => new SelectListItem
            {
                Text = x.StepTitle,
                Value = x.Id.ToString()
            }).OrderBy(o => o.Text).ToList();
        }


        public void Read(ProductionPlanningService productionPlanService, CustomerSubmittedRequirement requirment)
        {
            Id = requirment.Id;
            SubmittedRequirement = requirment;
            ProductCustomerDivision = requirment.Division;
            ProductSupplierId = requirment.SupplierId;
            ProductLocationId = requirment.LocationId;
            ProductCustomerName = requirment.Customer;
            ProductCustomerDivision = requirment.Division;
            ProductName = requirment.ProductName;
            ProductPartId = requirment.PartId;
            ProductProcedureId = requirment.ProcedureId;
            OldProductProcedureId = requirment.ProcedureId;
            OldProductProcedureId = requirment.ProcedureId;

            if (!string.IsNullOrWhiteSpace(requirment.QuoteJson))
            {
                HasQuote = true;
            }

            var attachedSteps = productionPlanService.GetStepsByObjectId(requirment.Id);

            if (attachedSteps.Any())
            {
                foreach (var step in attachedSteps)
                {
                    Steps.Add(new RequirementStepsDetailsViewModel
                    {
                        Id = step.Id,
                        ObjectId = step.ObjectId,
                        Process = step.Process,
                        Step = step.Step,
                        StandardDirectLaborMinutes = step.StandardDirectLaborMinutes,
                        StandardMachineMinutes = step.StandardMachineMinutes,
                        ReplacementCost = step.ReplacementCost,
                        Utilization = step.Utilization,
                        UsefulLife = step.UsefulLife,
                        EquipExpensePerMinute = step.EquipExpensePerMinute,
                        AnnualRM = step.AnnualRm,
                        RMPerMinute = step.RmPerMinute

                    });
                }
            }
        }
    }
}
