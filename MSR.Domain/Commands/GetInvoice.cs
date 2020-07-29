using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetInvoice : Command
    {
        public int Id { get; set; }
    }
}
