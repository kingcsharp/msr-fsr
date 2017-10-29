using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Msr.Models.ProductionPlanning;

namespace Msr.Services.ProductionPlanning.ViewModels
{
    public class RequirementStepsViewModel
    {
        public RequirementStepsViewModel()
        {
            requirementSteps = new List<RequirementStepsViewModel>();
        }
        public int Id { get; set; }
        public string ObjectId { get; set; }

        [Required]
        public string Process { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter numaric value only")]
        public int Step { get; set; }

        [Required]
        [Display(Name = "Standard Direct Labor Minutes")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter numaric value only")]
        public decimal StandardDirectLaborMinutes { get; set; }

        [Required]
        [Display(Name = "Standard Machine Minutes")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter numaric value only")]
        public decimal StandardMachineMinutes { get; set; }

       
        [Display(Name = "Replacement Cost")]
        
        public decimal ReplacementCost { get; set; }

       
        [Display(Name = "Utilization")]
        
        public decimal Utilization { get; set; }
        
        [Display(Name = "UsefulLife")]
        
        public decimal UsefulLife { get; set; }

        [Display(Name = "Equip Expense Per Minute")]
        
        public decimal EquipExpensePerMinute { get; set; }

        [Display(Name = "Annual RM")]
        
        public decimal AnnualRM { get; set; }

        [Display(Name = "RM Per Minute")]
        
        public decimal RMPerMinute { get; set; }

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

        public string PartKitNo { get; set; }

        public string postType { get; set; }

        public List<RequirementStepsViewModel> requirementSteps { get; set; }

        public List<SelectListItem> ProcessList { get; set; }

        public void Setup(List<RequirementStepsView> steps, string id)
        {
            ProductionPlanningService service = new ProductionPlanningService();
            foreach (var step in steps)
            {
                RequirementStepsViewModel addStep = new RequirementStepsViewModel();

                addStep.Id = step.Id;
                addStep.ObjectId = step.ObjectId;
                addStep.Process = step.Process;
                addStep.Step = step.Step;
                addStep.StandardDirectLaborMinutes = step.StandardDirectLaborMinutes;
                addStep.StandardMachineMinutes = step.StandardMachineMinutes;
                addStep.ReplacementCost = step.ReplacementCost;
                addStep.Utilization = step.Utilization;
                addStep.UsefulLife = step.UsefulLife;
                addStep.EquipExpensePerMinute = step.EquipExpensePerMinute;
                addStep.AnnualRM = step.AnnualRM;
                addStep.RMPerMinute = step.RMPerMinute;
                requirementSteps.Add(addStep);
            }
            ProcessList = new List<SelectListItem>
            {
                 new SelectListItem
                {
                    Text = "Select a Step...",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = "INCOMING INSPECTION",
                    Value = "INCOMING_INSPECTION"
                },
                new SelectListItem
                {
                    Text = "SERIALIZE",
                    Value = "SERIALIZE"
                },
                new SelectListItem
                {
                    Text = "BEAD BLAST",
                    Value = "BEAD_BLAST"
                },
                new SelectListItem
                {
                    Text = "SOAK - EXHAUSTED",
                    Value = "SOAK_EXHAUSTED"
                }
            };
            PartKitNo = service.GetPartKitNo(id);
        }

    }
}
