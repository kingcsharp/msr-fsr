namespace MSR.Answer.API.V1.Models
{
    public class UpdateWorkOrderPartRequest
    {
        public int WorkOrderPartId { get; set; }
        public string SerialNumber { get; set; }
        public string SegregationType { get; set; }
    }
}
