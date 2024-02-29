using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepMonitorApproval))]
    public partial class ProcedureStepMonitorApproval: TrackableEntity
    {
        public int ProcedureStepApprovalId { get; set; }

        public int MonitorTPLId { get; set; }

        public virtual ProcedureStepApproval ProcedureStepApproval { get; set; }

        private bool? _displayInReports;
        [Column(TypeName = "bool")]
        public bool? displayInReports { 
            get => _displayInReports ?? true;
            set => _displayInReports = value ?? true;
        }
    }
}
