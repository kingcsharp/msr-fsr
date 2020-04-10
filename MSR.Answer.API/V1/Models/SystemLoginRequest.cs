using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class SystemLoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
