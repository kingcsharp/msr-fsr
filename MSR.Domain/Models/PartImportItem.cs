using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Models
{ 
    public class PartImportItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public EnumSegregationType? SegregationType { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public bool IsKit { get; set; }
        public bool? IsActive { get; set; }
        public string NickName { get; set; }
        public int? MaximumCycles { get; set; }
    }
}