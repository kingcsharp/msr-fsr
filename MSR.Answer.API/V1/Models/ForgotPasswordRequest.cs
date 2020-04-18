using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string UserName { get; set; }
    }
}
