using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;
using System;

namespace MSR.Domain.Commands
{
    public class GetParts : PagingCommand
    {
        public int? Id { get;set;}
        public string Name { get; set; }
        public EnumSegregationType[]? SegregationType { get; set; }
        public string PartNumber { get; set; }
        public string OemPartNumber { get; set; }
        public bool? IsKit { get; set; }
        public bool? IsActive { get; set; }
        public int? MaximumCycles { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? lastUpdatedOn { get; set; }
        public string LastUpdatedByName { get; set; }
        public bool IncludeChildParts { get; set; } = true;
    }
}
