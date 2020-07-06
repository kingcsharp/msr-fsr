using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeactivateWorkflowModel : Command
    {
        public int Id { get; set; }
    }
}
