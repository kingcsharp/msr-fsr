using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateMenuRoleMap: Command
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }
    }
}
