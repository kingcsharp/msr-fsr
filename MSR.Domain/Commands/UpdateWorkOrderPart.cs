using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Commands
{
    public class UpdateWorkOrderPart : Command
    {
        public int WorkOrderPartId { get; set; }
        public string SerialNumber { get; set; }
        public EnumSegregationType? SegregationType { get; set; }
    }
}
