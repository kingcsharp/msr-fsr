namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class MenuRolePermission: TrackableEntity
    {
        public virtual MenuRole MenuRole { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanActivate { get; set; }
        public bool CanApprove { get; set; }
        public bool CanDelete { get; set; }

    }
}
