using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class ImportPartsRequest
    {
        [Required]
        public string base64Data { get; set; }
    }
}
