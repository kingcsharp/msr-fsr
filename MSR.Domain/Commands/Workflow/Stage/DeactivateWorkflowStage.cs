using MSR.Domain.Commanding;

namespace MSR.Domain.Commands.Workflow
{
    public class DeactivateWorkflowStage : Command
    {
        public int Id { get; set; }
    }
}
