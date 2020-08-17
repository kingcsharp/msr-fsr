using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateFile : Command
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string Name { get; set; }
        public string Base64String { get; set; }
        public string ContentType { get; set; }
    }
}
