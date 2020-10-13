using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateProductStepRequest
    {
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
