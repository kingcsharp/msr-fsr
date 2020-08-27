using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteProcedureStepTemplate : Command
    {
        public int Id { get; set; }
    }
}
