using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CopyProcedure : Command
    {
        public int SourceProcedureId { get; set; }
    }
}
