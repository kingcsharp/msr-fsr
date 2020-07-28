using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(InvoiceItem))]
    public partial class InvoiceItem: TrackableEntity
    {
        public int InvoiceId { get; set; }

        public int PurchaseOrderId { get; set; }

        public int WorkOrderId { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
