using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteUserRole: Command
    {
        public int UserRoleId { get; set; }
    }
}
