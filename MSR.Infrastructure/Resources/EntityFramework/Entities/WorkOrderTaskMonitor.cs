using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrderTaskMonitor))]
    public partial class WorkOrderTaskMonitor: TrackableEntity
    {
        public int? ProcedureMonitorId { get; set; }

        [ForeignKey("ProcedureMonitorId")]
        public virtual ProcedureStepMonitor ProcedureStepMonitor { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal? NumVal { get; set; }

        public string TextVal { get; set; }

        public string MultiVal { get; set; }

        public int SensorMappingId { get; set; }

        public string Comment { get; set; }

        public int WorkOrderTaskId { get; set; }

        public virtual WorkOrderTask WorkOrderTask { get; set; }
        public string Description { get; set; }
        public int? MonitorListId { get; set; }
        public string ShouldBe { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal? HighTarget { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal? LowTarget { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal? Target { get; set; }

        public string FailAction { get; set; }
        public string SensorName { get; set; }
        public int? MonitorTypeId { get; set; }
        public int? InputTypeId { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal? LowNumVal { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal? HighNumVal { get; set; }
    }
}
