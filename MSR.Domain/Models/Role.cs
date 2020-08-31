using MSR.Domain.Models.BaseModels;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class Role: TrackableModel
    {
        public Role()
        {
            Menus = new HashSet<MenuItem>();
            ParentRoles = new HashSet<Role>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public bool? IsCertificationRole { get; set; }

        public ICollection<MenuItem> Menus { get; set; }

        public Permission Permissions { get; set; }
        public Permission InheritedPermissions { get; set; }

        public ICollection<Role> ParentRoles { get; set; }

        public bool HasAssignedUsers { get; set; }
    }
}
