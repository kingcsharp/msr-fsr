using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PurchaseApproval))]
    public partial class PurchaseApproval: TrackableEntity
    {
        public int PurchaseId { get; set; }

        public int StatusId { get; set; }

        public int PurchaseOrderId { get; set; }

        public int PurchaseOrderProductId { get; set; }

        [StringLength(50)]
        public string CustomerPurchaseNumber { get; set; }

        public int LocationId { get; set; }

        public int Qty { get; set; }

        [Column(TypeName = "money")]
        public decimal PurchasePrice { get; set; }
    }
}
