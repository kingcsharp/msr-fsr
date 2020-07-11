using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetProcedureStepTemplate : Command
    {
        public int? Id { get; set; }
    }
}
