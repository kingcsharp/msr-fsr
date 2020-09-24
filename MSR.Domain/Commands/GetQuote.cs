using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetQuote : Command
    {
        public int? Id { get; set; }
    }
}
