namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class RoleChildRoleMap: TrackableEntity
    {
        public virtual Role ParentRole { get; set; }
        public int ParentRoleId { get; set; }
        public virtual Role ChildRole { get; set; }
        public int? ChildRoleId { get; set; }
    }
}
