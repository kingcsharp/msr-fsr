using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class ImportParts : Command
    {
        public string base64Data { get; set; }
    }

    public class PartCSVRecord
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public string NickName { get; set; }
        public int MaximumCycles { get; set; }
    }
}
