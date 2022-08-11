using MSR.Domain.Commanding.Enums;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateWorkOrderPartRequest
    {
        public int WorkOrderPartId { get; set; }
        public string SerialNumber { get; set; }
        public string PartData { get; set; }
        public EnumSegregationType? SegregationType { get; set; }
    }
}
