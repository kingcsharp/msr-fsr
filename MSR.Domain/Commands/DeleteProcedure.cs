using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteProcedure : Command
    {
        public int? procedureID;
    }
}
