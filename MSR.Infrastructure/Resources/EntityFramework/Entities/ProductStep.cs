using MSR.Infrastructure.Resources.Services.Part;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProductStep))]
    public partial class ProductStep : Entity
    {
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// Associated Procedure Step ID, if any
        /// </summary>
        /// <description>
        /// Associated Procedure Step ID, if any.  This field can
        /// be null.  A product step does not necessarily have a procedure step.
        /// </description>
        public int? ProcedureStepId { get; set; }

        public int? LaborMinutes { get; set; }

        public int? EquipmentMinutes { get; set; }


        [Column(TypeName = "money")]
        public decimal? ReplacementCost { get; set; }

        public float? Utilization { get; set; }

        public int? UsefulLife { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? EquipmentExpensePerMinute { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? RMAnnualRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? RMPerMinuteRate { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public int? PrintOrder { get; set; }

        public virtual ProcedureStep ProcedureStep { get; set; }

        public virtual Product Product{ get; set; }

    }
}
