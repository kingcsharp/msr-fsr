using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetMonitorModel : Command
    {
        public int? procedureStepId;
        public int? procedureStepMonitorId;
    }
}
