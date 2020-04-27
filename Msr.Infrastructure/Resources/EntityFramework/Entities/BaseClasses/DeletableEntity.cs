namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class DeletableEntity: TrackableEntity 
    {
        public virtual bool IsActive { get; set; }
    }
}
