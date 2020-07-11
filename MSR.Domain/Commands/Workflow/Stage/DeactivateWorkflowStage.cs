using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeactivateWorkflowStage : Command
    {
        public int Id { get; set; }
    }
}
