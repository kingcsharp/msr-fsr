namespace MSR.Domain.Models
{
    public class FileModel
    {
        public int? FileId { get; set; }
        public int? EntityId { get; set; }
        public string Name { get; set; }
        public string Base64String { get; set; }
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public string FileURL { get; set; }
    }
}
