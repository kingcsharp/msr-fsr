using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepMonitorApproval))]
    public partial class ProcedureStepMonitorApproval: TrackableEntity
    {
        public int ProcedureStepApprovalId { get; set; }

        public int MonitorTPLId { get; set; }

        public virtual MonitorTemplate MonitorTemplate { get; set; }

        public virtual ProcedureStepApproval ProcedureStepApproval { get; set; }
    }
}
