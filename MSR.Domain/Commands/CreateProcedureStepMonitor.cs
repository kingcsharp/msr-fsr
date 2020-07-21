using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateProcedureStepMonitor : Command
    {
        public int ProcedureStepId { get; set; }
        public int MonitorTypeId { get; set; }
        public int InputTypeId { get; set; }
        public string Description { get; set; }
        public int? MonitorListId { get; set; }
        public string ShouldBe { get; set; }
        public double? HighTarget { get; set; }
        public double? LowTarget { get; set; }
        public double? Target { get; set; }
        public string FailAction { get; set; }
        public int SensorMappingId { get; set; }
        public bool? SendNCREmail { get; set; }
    }
}
