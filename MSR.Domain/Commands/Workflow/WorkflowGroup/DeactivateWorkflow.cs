using MSR.Domain.Commanding;

namespace MSR.Domain.Commands.Workflow
{
    public class DeactivateWorkflow : Command
    {
        public int Id { get; set; }
    }
}
