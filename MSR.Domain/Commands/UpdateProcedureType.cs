using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UpdateProcedureType : Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
