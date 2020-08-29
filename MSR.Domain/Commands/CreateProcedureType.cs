using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateProcedureType : Command
    {
        public string Name { get; set; }
        public string MajorGroup { get; set; }
    }
}
