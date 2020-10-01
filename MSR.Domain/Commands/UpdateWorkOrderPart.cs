using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UpdateWorkOrderPart : Command
    {
        public int WorkOrderPartId { get; set; }
        public string SerialNumber { get; set; }
    }
}
