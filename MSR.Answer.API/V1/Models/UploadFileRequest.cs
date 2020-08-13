using Microsoft.AspNetCore.Http;

namespace MSR.Answer.API.V1.Models
{
    public class UploadFileRequest
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public IFormFile Image { get; set; }
    }
}
