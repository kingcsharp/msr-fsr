using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetProcedureStepMonitor : Command
    {
        public int? procedureStepId;
        public int? procedureStepMonitorId;
    }
}
