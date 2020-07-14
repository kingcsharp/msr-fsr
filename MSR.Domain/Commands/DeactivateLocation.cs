using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeactivateLocation: Command
    {
        public int LocationId { get; set; }
    }
}
