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
        public decimal? ReplacementCost { get; set; }
        public float? Utilization { get; set; }
        public int? UsefulLife { get; set; }
        public decimal? EquipmentExpensePerMinute { get; set; }
        public decimal? RMAnnualRate { get; set; }
        public decimal? RMPerMinuteRate { get; set; }
        public int? ProductStepId { get; set; }


    }
}
