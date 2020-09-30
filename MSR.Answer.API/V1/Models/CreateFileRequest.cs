using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateFileRequest
    {
        [Required]
        public string EntityName { get; set; }
        [Required]
        public int? EntityId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Base64String { get; set; }
        [Required]
        public string ContentType { get; set; }
    }
}
