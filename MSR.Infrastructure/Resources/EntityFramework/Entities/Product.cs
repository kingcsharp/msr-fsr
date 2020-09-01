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

        [Required]
        public int Revision { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProcedureId { get; set; }

        [Required]
        public int PartId { get; set; }

        [Column(TypeName ="money")]
        public decimal? LaborCost { get; set; }

        [Column(TypeName = "money")]
        public decimal? EquipmentCost { get; set; }

        [Column(TypeName = "money")]
        public decimal? MaterialCost { get; set; }

        [Column(TypeName = "money")]
        public decimal? SalesTax { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal TotalSalePrice { get; set; }

        public int? CycleTime { get; set; }

        // TODO: there is no such field
        //public int? CustomerRequirementId { get; set; }

        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
