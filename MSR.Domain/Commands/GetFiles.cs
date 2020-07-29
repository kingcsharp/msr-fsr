using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetFiles : Command
    {
        public string entityName;
        public int entityId;
        public int? fileId;
    }
}
