using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateCycleCountHistory : Command
    {
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; }
        public int CycleCount { get; set; }
    }
}
