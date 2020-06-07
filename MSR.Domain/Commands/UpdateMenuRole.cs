using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UpdateMenuRoleMap: Command
    {
        public int MenuRoleId { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanActivate { get; set; }
        public bool CanApprove { get; set; }
    }
}
