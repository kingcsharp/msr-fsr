using System;

namespace MSR.Domain.Models.BaseClasses
{
    public abstract class TrackableEntity : DeletableEntity
    {
        public virtual DateTime LastUpdatedOn { get; set; }
        public virtual int? LastUpdatedBy { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual int? CreatedBy { get; set; }
    }
}
