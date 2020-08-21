using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteProcedureStep : Command
    {
        public int procedureID { get; set; }
        public int procedureStepID { get; set; }
    }
}
