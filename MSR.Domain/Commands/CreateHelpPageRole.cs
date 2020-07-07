using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateHelpPageRole: Command
    {
        public int HelpPageId { get; set; }
        public int RoleId { get; set; }
    }
}
