using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateWorkOrder : Command
    {
        public string Name { get; set; }
    }
}
