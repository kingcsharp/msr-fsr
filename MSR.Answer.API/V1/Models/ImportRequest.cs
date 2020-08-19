using MSR.Domain.Commanding.Enums;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class ImportRequest
    {
        [Required]
        public string Base64Data { get; set; }
        [Required]
        public EnumMenuItem MenuItem { get; set; }
    }
}
