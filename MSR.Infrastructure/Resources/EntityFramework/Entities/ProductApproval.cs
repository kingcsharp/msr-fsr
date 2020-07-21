using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProductApproval))]
    public partial class ProductApproval: ApprovalEntity
    {
        public int ProductId { get; set; }

        public int Revision { get; set; }

        public int CustomerId { get; set; }

        public int? CustomerRequirementId { get; set; }

        public int ProcedureId { get; set; }

        public int PartId { get; set; }

        [Column(TypeName = "money")]
        public decimal EquipmentCost { get; set; }

        [Column(TypeName = "money")]
        public decimal MaterialCost { get; set; }

        [Column(TypeName = "money")]
        public decimal? SalesTax { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalSalePrice { get; set; }

        public int? CycleTime { get; set; }
    }
}
