using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteQuote : Command
    {
        public int Id { get; set; }
    }
}
