using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeactivateCustomer: Command
    {
        public int CustomerId { get; set; }
    }
}
