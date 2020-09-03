using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UpdateProcedureStepMonitor : Command
    {
        public int Id { get; set; }
        public int? ProcedureStepId { get; set; }
        public string MonitorType { get; set; }
        public int? MonitorTypeId { get; set; } // overridden by MonitorType
        public string InputType { get; set; }
        public int? InputTypeId { get; set; } // overridden by InputType
        public string Description { get; set; }
        public int? MonitorListId { get; set; }
        public string ShouldBe { get; set; }
        public float? HighTarget { get; set; }
        public float? LowTarget { get; set; }
        public float? Target { get; set; }
        public string FailAction { get; set; }
        public int? SensorMappingId { get; set; }
        public bool? SendNCREmail { get; set; }
    }
}
