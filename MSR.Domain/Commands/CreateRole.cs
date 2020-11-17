using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateRole: Command
    {
        public string Name { get; set; }
        public bool IsCertificationRole { get; set; }
        public ICollection<int> ParentRoleIds { get; set; }
        public ICollection<UserRoleModel> UserRoles { get; set; }
    }
}
