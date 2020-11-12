using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProductStepApproval))]
    public partial class ProductStepApproval : Entity
    {
        public int ProductApprovalId { get; set; }
        public virtual ProductApproval ProductApproval { get; set; }

        public int? ProductId { get; set; }
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
        public int? ProductStepId { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public int? PrintOrder { get; set; }
    }
}
