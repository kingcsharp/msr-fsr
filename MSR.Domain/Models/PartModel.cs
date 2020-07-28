using System.Collections.Generic;

using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class PartModel : TrackableModel
    {
        public PartModel()
        {
            CreateSubParts = new HashSet<SubPartModel>();
            Files = new HashSet<FileModel>();
            IsPending = false;
        }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public bool IsKit { get; set; }
        public string NickName { get; set; }
        public int? MaximumCycles { get; set; }
        public string CreatedByName { get; set; }
        public string LastUpdatedByName { get; set; }
        public virtual ICollection<SubPartModel> CreateSubParts { get; set; }
        public virtual ICollection<FileModel> Files { get; set; }
        public bool? IsActive { get; set; }

        // Set to "true" if the system put the data in
        // the PartApproval table, and there is now new Part row.
        public bool IsPending;
    }
}
