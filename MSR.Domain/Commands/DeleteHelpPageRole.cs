using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteHelpPageRole: Command
    {
        public int HelpPageId { get; set; }
        public int roleId { get; set; }
    }
}
