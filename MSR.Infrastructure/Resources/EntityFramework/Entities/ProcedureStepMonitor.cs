using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepMonitor))]
    public partial class ProcedureStepMonitor: TrackableEntity
    {
        public int ProcedureStepId { get; set; }

        public int MonitorTPLId { get; set; }

        public virtual MonitorTemplate MonitorTemplate { get; set; }

        public virtual ProcedureStep ProcedureStep { get; set; }
    }
}
