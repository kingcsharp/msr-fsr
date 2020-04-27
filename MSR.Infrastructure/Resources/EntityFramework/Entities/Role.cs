using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Role))]
    public partial class Role: TrackableEntity
    {
        public Role()
        {
            Users = new HashSet<UserRole>();
            Menus = new HashSet<MenuRole>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool? IsCertificationRole { get; set; }

        [StringLength(100)]
        public string OldId { get; set; }

        public ICollection<UserRole> Users { get; set; }

        public ICollection<MenuRole> Menus { get; set; }
    }
}
