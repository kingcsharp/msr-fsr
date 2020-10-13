using System.ComponentModel.DataAnnotations;

namespace MSR.Domain.Commands
{
    public class UpdateProductStep
    {
        public int? ProductId { get; set; }

        [Required]
        public int? ProcedureStepId { get; set; }

        public int? LaborMinutes { get; set; }

        public int? EquipmentMinutes { get; set; }

        public decimal? ReplacementCost { get; set; }

        public float? Utilization { get; set; }

        public int? UsefulLife { get; set; }

        public decimal? EquipmentExpensePerMinute { get; set; }

        public decimal? RMAnnualRate { get; set; }

        public decimal? RMPerMinuteRate { get; set; }
    }
}
