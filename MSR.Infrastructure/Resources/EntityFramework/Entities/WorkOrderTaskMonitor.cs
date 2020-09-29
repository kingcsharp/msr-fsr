using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrderTaskMonitor))]
    public partial class WorkOrderTaskMonitor: TrackableEntity
    {
        public int ProcedureMonitorId { get; set; }

        public int? NumVal { get; set; }

        public string TextVal { get; set; }

        public string MultiVal { get; set; }

        public int SensorMappingId { get; set; }

        public string Comment { get; set; }

        public int WorkOrderTaskId { get; set; }

        public virtual WorkOrderTask WorkOrderTask { get; set; }
    }
}
