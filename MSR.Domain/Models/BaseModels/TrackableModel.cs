using System;

namespace MSR.Domain.Models.BaseModels
{
    public abstract class TrackableModel : EntityModel
    {
        public DateTime? LastUpdatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
    }
}