using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
