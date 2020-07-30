using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class TrackableEntity : CreatableEntity
    {
        public virtual DateTime? LastUpdatedOn { get; set; }
        public virtual int? LastUpdatedBy { get; set; }
        [ForeignKey("LastUpdatedBy")]
        public virtual User LastUpdated { get; set; }
    }
}
