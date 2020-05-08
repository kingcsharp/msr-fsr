using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(MenuRolePermission))]
    public class MenuRolePermission: TrackableEntity
    {
        public int MenuRoleId { get; set; }
        public virtual MenuRole MenuRole { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanActivate { get; set; }
        public bool CanApprove { get; set; }
        public bool CanDelete { get; set; }

    }
}
