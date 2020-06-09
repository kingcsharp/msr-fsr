using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetCustomer: Command
    {
        public int Id { get; set; }
    }
}
