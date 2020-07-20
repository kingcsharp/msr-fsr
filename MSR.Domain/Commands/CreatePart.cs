using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreatePart : Command
    {
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public int Qty { get; set; }
        public bool? IsKit { get; set; }
        public string NickName { get; set; }
        public int? ParentId { get; set; }
        public int? MaximumCycles { get; set; }
    }
}
