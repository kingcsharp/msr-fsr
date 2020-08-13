using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteLocationSensorMap : Command
    {
        public int LocationId { get; set; }
        public int SensorItemId { get; set; }
    }
}
