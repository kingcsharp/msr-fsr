using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetRoleUsers : Command
    {
        public int RoleId { get; set; }
    }
}
