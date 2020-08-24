using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteProcedureType : Command
    {
        public int id { get; set; }
    }
}
