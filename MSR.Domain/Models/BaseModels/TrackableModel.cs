using System;

namespace MSR.Domain.Models.BaseModels
{
    public abstract class TrackableModel : CreatableModel
    {
        public DateTime? LastUpdatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
        public User LastUpdated { get; set; }
    }
}