using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetLocations : Command
    {
        public int? ParentId { get; set; }
        public int? Id { get; set; }
        public string? InternalAddress { get; set; }
    }
}
