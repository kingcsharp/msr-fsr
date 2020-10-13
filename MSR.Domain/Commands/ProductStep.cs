using System.ComponentModel.DataAnnotations;

namespace MSR.Domain.Models
{
    public class ProductStep
    {
        public int? ProductId { get; set; }

        [Required]
        public int? ProcedureStepId { get; set; }

        [Required]
        public int? LaborMinutes { get; set; }

        public int? EquipmentMinutes { get; set; }

        public decimal? ReplacementCost { get; set; }

        public float? Utilization { get; set; }

        public int? UsefulLife { get; set; }

        public decimal? EquipmentExpensePerMinute { get; set; }

        public decimal? RMAnnualRate { get; set; }

        public decimal? RMPerMinuteRate { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public int? PrintOrder { get; set; }

    }
}
