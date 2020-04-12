namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class DeletableEntity : Entity
    {
        public virtual bool IsActive { get; set; }
    }
}
