using System.Collections.Generic;

using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class PartModel : TrackableModel
    {
        public PartModel() { }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public int Qty { get; set; }
        public bool? IsKit { get; set; }
        public string NickName { get; set; }
        public int? ParentId { get; set; }
        public int? MaximumCycles { get; set; }
        public string CreatedByName { get; set; }
        public string LastUpdatedByName { get; set; }
    }
}
