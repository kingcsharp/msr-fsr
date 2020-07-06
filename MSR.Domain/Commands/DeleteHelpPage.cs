using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteHelpPage: Command
    {
        public int HelpPageId { get; set; }
    }
}
