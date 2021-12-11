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
        public decimal? HighTarget { get; set; }
        public decimal? LowTarget { get; set; }
        public decimal? Target { get; set; }
        public string FailAction { get; set; }
        public string SensorName { get; set; }
        public bool? SendNCREmail { get; set; }
    }
}
