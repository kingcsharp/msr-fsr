namespace MSR.Answer.API.V1.Models
{
    public class GetFileRequest
    {
        public string EntityName { get; set; }
        public int? EntityId { get; set; }
        public int? FileId { get; set; }
    }
}
