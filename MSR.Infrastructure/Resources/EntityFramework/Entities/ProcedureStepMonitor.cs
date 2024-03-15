using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepMonitor))]
    public partial class ProcedureStepMonitor: TrackableEntity
    {
        public int? ProcedureStepId { get; set; }

        public int MonitorTypeId { get; set; }
        public MonitorType MonitorType { get; set; }

        public int InputTypeId { get; set; }
        public MonitorInputType InputType { get; set; }

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
        public bool? SendNCREmail { get; set; }
        public int? UnitofMeasureId { get; set; }

        public virtual ProcedureStep ProcedureStep { get; set; }
    }
}
