using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PurchaseOrder))]
    public class PurchaseOrder : TrackableEntity
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferencePO { get; set; }

        [Required]
        [StringLength(100)]
        public string ReferenceName { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }
        public int? Revision { get; set; }

        public DateTime? CloseDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalPurchaseLimit { get; set; }

        [StringLength(100)]
        public string CustomerReference { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
    }
}
