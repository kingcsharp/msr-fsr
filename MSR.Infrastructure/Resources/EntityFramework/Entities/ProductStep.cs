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

        [Required]
        public int ProcedureStepId { get; set; }

        public int? LaborMinutes { get; set; }

        public int? EquipmentMinutes { get; set; }


        [Column(TypeName = "money")]
        public decimal? ReplacementCost { get; set; }

        public float? Utilization { get; set; }

        public int? UsefulLife { get; set; }

        public decimal? EquipmentExpensePerMinute { get; set; }

        public decimal? RMAnnualRate { get; set; }

        public decimal? RMPerMinuteRate { get; set; }

        public virtual ProcedureStep ProcedureStep { get; set; }

        public virtual Product Product{ get; set; }

    }
}
