using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class UpdateRole : CreateRole
    {
        public int Id { get; set; }
        public ICollection<UserRoleModel> UserRoles { get; set; }
    }
}
