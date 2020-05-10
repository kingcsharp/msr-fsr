using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrderTaskMonitor))]
    public partial class WorkOrderTaskMonitor: TrackableEntity
    {
        public int WorkOrderTaskId { get; set; }

        public int MonitorTPLId { get; set; }

        [StringLength(255)]
        public string Result { get; set; }

        public string Comment { get; set; }
        public virtual MonitorTemplate MonitorTemplate { get; set; }
        public virtual WorkOrderTask WorkOrderTask { get; set; }
    }
}
