using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeactivateUser: Command
    {
        public int AccountId { get; set; }
    }
}
