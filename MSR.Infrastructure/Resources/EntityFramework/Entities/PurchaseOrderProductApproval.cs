using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(PurchaseOrderProductApproval))]
    public partial class PurchaseOrderProductApproval: TrackableEntity
    {
        public int PurchaseOrderApprovalId { get; set; }
    }
}
