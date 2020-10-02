using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetProduct : Command
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
    }
}
