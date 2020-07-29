using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DetachFile : Command
    {
        public int entityId { get; set; }
        public string entityName { get; set; }
        public int? fileId { get; set; }
    }
}
