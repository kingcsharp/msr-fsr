using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetSearch: Command
    {
        public string SearchTerm { get; set; }
    }
}
