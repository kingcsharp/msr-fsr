using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table("MenuRole")]
    public partial class MenuRole: TrackableEntity
    {
        public int? MenuItemId { get; set; }

        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
