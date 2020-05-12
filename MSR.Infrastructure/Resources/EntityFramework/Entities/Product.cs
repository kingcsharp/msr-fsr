using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Product))]
    public partial class Product: TrackableEntity
    {
        public Product()
        {
            WorkOrders = new HashSet<WorkOrder>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

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

        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
