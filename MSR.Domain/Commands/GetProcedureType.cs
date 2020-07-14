using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetProcedureType : Command
    {
        public int? Id { get; set; }
    }
}
