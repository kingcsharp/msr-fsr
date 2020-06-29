using MSR.Domain.Commanding;

namespace MSR.Domain.Commands.Workflow
{
    public class DeactivateWorkflowGroup : Command
    {
        public int Id { get; set; }
    }
}
