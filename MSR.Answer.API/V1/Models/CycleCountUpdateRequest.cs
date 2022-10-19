namespace MSR.Answer.API.V1.Models
{
    public class CycleCountUpdateRequest
    {
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; }
        public int CycleCount { get; set; }
    }
}
