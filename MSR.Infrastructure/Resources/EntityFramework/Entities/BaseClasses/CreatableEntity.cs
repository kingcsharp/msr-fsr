using System;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public abstract class CreatableEntity : Entity
    {
        public virtual DateTime CreatedOn { get; set; }
        public virtual int? CreatedBy { get; set; }
    }
}
