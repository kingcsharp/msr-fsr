using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetProcedureStepType : Command
    {
        public int? Id { get; set; }
    }
}
