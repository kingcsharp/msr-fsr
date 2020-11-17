using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetRolesUsers : Command
    {
        public int? RoleId { get; set; }
    }
}
