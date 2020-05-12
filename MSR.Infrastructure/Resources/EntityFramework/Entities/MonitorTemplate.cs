using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(MonitorTemplate))]
    public partial class MonitorTemplate: TrackableEntity
    {
        public MonitorTemplate()
        {
            ProcedureStepMonitorApprovals = new HashSet<ProcedureStepMonitorApproval>();
            ProcedureStepMonitors = new HashSet<ProcedureStepMonitor>();
            WorkOrderTaskMonitors = new HashSet<WorkOrderTaskMonitor>();
        }

        [Required]
        [StringLength(20)]
        public string MonitorType { get; set; }

        [Required]
        [StringLength(20)]
        public string InputType { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(20)]
        public string ListSource { get; set; }

        [StringLength(20)]
        public string ShouldBe { get; set; }

        public float? HighestThreshold { get; set; }

        public float? HighThreshold { get; set; }

        public float? LowestThreshold { get; set; }

        public float? LowThreshold { get; set; }

        public float? Target { get; set; }

        [Required]
        [StringLength(20)]
        public string FailAction { get; set; }

        public int SensorMappingId { get; set; }

        public bool? SendNCREmail { get; set; }

        public virtual ICollection<ProcedureStepMonitorApproval> ProcedureStepMonitorApprovals { get; set; }
        public virtual ICollection<ProcedureStepMonitor> ProcedureStepMonitors { get; set; }
        public virtual ICollection<WorkOrderTaskMonitor> WorkOrderTaskMonitors { get; set; }
    }
}
