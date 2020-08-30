using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Purchase))]
    public partial class Purchase: TrackableEntity
    {
        public Purchase()
        {
            WorkOrders = new HashSet<WorkOrder>();
        }

        public int PurchaseOrderId { get; set; }

        public int PurchaseOrderProductId { get; set; }

        [StringLength(50)]
        public string CustomerPurchaseNumber { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        [StringLength(20)]
        public string SerialNumber { get; set; }

        [Required]
        public int Qty { get; set; }

        public int? CustomerLineNumber { get; set; }

        [StringLength(10)]
        public string MTTN { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal PurchasePrice { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        public virtual ICollection<WorkOrder> WorkOrders { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey("PurchaseOrderProductId")]
        public virtual PurchaseOrderProduct PurchaseOrderProduct { get; set; }
    }
}
