using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateProcedureStepMonitor : Command
    {
        public string Name { get; set; }
        public int? ProcedureStepMonitorTypeId { get; set; }
        public int? Revision { get; set; }
        public double? Duration { get; set; }
        public string DurationType { get; set; }
    }
}
