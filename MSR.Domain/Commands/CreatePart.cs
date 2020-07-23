using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreatePart : Command
    {
        public CreatePart()
        {
            SubParts = new HashSet<SubPartModel>();
        }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public string NickName { get; set; }
        public int? MaximumCycles { get; set; }
        public ICollection<SubPartModel> SubParts { get; set; }
        public string Comment { get; set; }
        public List<File> Files { get; set; }
    }
}
