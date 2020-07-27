using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class RemoveMenuRoleMap: Command
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
    }
}
