using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PurchaseOrderProduct))]
    public partial class PurchaseOrderProduct: TrackableEntity
    {
        public int PurchaseOrderId { get; set; }

        public int ProductId { get; set; }

        public int ProductRevision { get; set; }
    }
}
