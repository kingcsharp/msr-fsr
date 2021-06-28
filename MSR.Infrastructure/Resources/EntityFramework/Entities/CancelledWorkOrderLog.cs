using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(CancelledWorkOrderLog))]
    public partial class CancelledWorkOrderLog: CreatableEntity
    {
        public int WorkOrderId { get; set; }
        public bool WasInvoiced { get; set; }
    }
}
