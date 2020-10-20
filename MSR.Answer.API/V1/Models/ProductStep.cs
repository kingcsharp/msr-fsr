using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class ProductStep
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets ProductId
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or Sets ProcedureStepId
        /// </summary>
        [Required]
        public int? ProcedureStepId { get; set; }

        /// <summary>
        /// Gets or Sets LaborMinutes
        /// </summary>
        [Required]
        public int? LaborMinutes { get; set; }

        /// <summary>
        /// Gets or Sets EquipmentMinutes
        /// </summary>
        public int? EquipmentMinutes { get; set; }

        /// <summary>
        /// Gets or Sets ReplacementCost
        /// </summary>
        public decimal? ReplacementCost { get; set; }

        /// <summary>
        /// Gets or Sets Utilization
        /// </summary>
        public float? Utilization { get; set; }

        /// <summary>
        /// Gets or Sets UsefulLife
        /// </summary>
        public int? UsefulLife { get; set; }

        /// <summary>
        /// Gets or Sets EquipmentExpensePerMinute
        /// </summary>
        public decimal? EquipmentExpensePerMinute { get; set; }

        /// <summary>
        /// Gets or Sets RMAnnualRate
        /// </summary>
        public decimal? RMAnnualRate { get; set; }

        /// <summary>
        /// Gets or Sets RMPerMinuteRate
        /// </summary>
        public decimal? RMPerMinuteRate { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// PrintOrder
        /// </summary>
        public int? PrintOrder { get; set; }

    }
}
