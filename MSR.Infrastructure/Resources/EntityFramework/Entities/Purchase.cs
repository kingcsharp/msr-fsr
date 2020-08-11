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

        public int LocationId { get; set; }

        public int Qty { get; set; }

        [Column(TypeName = "money")]
        public decimal PurchasePrice { get; set; }

        public virtual ICollection<WorkOrder> WorkOrders { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
    }
}
