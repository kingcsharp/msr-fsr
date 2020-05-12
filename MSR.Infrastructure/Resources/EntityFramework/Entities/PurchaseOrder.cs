using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PurchaseOrder))]
    public partial class PurchaseOrder: TrackableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferencePO { get; set; }

        [Required]
        [StringLength(100)]
        public string ReferenceName { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalPurchaseLimit { get; set; }

        [StringLength(100)]
        public string CustomerReference { get; set; }
    }
}
