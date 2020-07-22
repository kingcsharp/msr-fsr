using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(HelpPageRoleMap))]
    public class HelpPageRoleMap: TrackableEntity
    {
        public int HelpPageId { get; set; }
        public virtual HelpPage HelpPage { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
