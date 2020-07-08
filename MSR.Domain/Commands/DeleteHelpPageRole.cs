using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteHelpPageRole: Command
    {
        public int HelpPageRoleId { get; set; }
    }
}
