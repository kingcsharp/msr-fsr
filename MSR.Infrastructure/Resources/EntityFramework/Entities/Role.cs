using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Role))]
    public partial class Role : TrackableEntity
    {
        public Role()
        {
            Users = new HashSet<UserRole>();
            Menus = new HashSet<MenuRole>();
            HelpPages = new HashSet<HelpPageRoleMap>();
            ChildRoles = new HashSet<RoleChildRoleMap>();
            ParentRoles = new HashSet<RoleChildRoleMap>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool? IsCertificationRole { get; set; }

        [StringLength(100)]
        public string OldId { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }

        public virtual ICollection<MenuRole> Menus { get; set; }

        public virtual ICollection<HelpPageRoleMap> HelpPages { get; set; }

        public virtual ICollection<RoleChildRoleMap> ChildRoles { get; set; }
        public virtual ICollection<RoleChildRoleMap> ParentRoles { get; set; }
    }
}
