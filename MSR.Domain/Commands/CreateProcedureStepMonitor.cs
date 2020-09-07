using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateProcedureStepMonitor : Command
    {
        public int ProcedureStepId { get; set; }
        public string MonitorType { get; set; }
        public string InputType { get; set; }
        public string Description { get; set; }
        public int? MonitorListId { get; set; }
        public string ShouldBe { get; set; }
        public float? HighTarget { get; set; }
        public float? LowTarget { get; set; }
        public float? Target { get; set; }
        public string FailAction { get; set; }
        public string SensorName { get; set; }
        public bool? SendNCREmail { get; set; }
    }
}
