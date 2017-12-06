using System.ComponentModel.DataAnnotations;

namespace Msr.Services.ProductionPlanning.ViewModels
{
    public class RequirementStepsDetailsViewModel
    {
        public int Id { get; set; }
        public string ObjectId { get; set; }

        public string Process { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter numaric value only")]
        public int Step { get; set; }

        [Display(Name = "Standard Direct Labor Minutes")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter numaric value only")]
        public decimal? StandardDirectLaborMinutes { get; set; }

        [Display(Name = "Standard Machine Minutes")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter numaric value only")]
        public decimal? StandardMachineMinutes { get; set; }

        [Display(Name = "Replacement Cost")]

        public decimal? ReplacementCost { get; set; }

        [Display(Name = "Utilization")]

        public decimal? Utilization { get; set; }

        [Display(Name = "UsefulLife")]

        public decimal? UsefulLife { get; set; }

        [Display(Name = "Equip Expense Per Minute")]

        public decimal? EquipExpensePerMinute { get; set; }

        [Display(Name = "Annual RM")]

        public decimal? AnnualRM { get; set; }

        [Display(Name = "RM Per Minute")]

        public decimal? RMPerMinute { get; set; }

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
    }
}
