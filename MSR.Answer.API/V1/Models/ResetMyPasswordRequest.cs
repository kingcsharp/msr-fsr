using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class ResetMyPasswordRequest
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string OldPassword { get; set; }
    }
}