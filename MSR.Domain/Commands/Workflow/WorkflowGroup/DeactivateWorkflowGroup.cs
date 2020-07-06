using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeactivateWorkflowGroup : Command
    {
        public int Id { get; set; }
    }
}
