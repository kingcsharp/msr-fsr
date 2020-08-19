using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetSensorsForLocation: Command
    {
        public int LocationId { get; set; }
    }
}
