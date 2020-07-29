using System;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public abstract class TrackableEntity : CreatableEntity
    {
        public virtual DateTime? LastUpdatedOn { get; set; }
        public virtual int? LastUpdatedBy { get; set; }
    }
}
