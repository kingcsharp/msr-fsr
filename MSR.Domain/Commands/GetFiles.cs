using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetFiles : Command
    {
        public string entityName { get; set; }
        public int? entityId { get; set; }
        public int? fileId { get; set; }
    }
}
