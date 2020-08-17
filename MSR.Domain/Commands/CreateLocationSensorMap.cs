using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateLocationSensorMap: Command
    {
        public int LocationId { get; set; }
        public int SensorItemId { get; set; }
    }
}
