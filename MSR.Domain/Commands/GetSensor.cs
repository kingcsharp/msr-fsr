
using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetSensor: Command
    {
        public int? SensorId { get; set; }
    }
}
