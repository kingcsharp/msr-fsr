using MSR.Domain.Commanding;

namespace MSR.Domain.Commands.Workflow
{
    public class DeactivateWorkflowModel : Command
    {
        public int Id { get; set; }
    }
}
