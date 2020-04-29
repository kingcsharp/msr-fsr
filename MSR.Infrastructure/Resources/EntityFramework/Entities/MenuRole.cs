using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public partial class MenuRole: TrackableEntity
    {
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual MenuRolePermission MenuRolePermission { get; set; }
    }
}
