using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetProcedureStep : Command
    {
        public int? procedureId { get; set; }
        public int? stepId { get; set; }
    }
}
