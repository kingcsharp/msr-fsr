using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class SystemLoginRequest
    {
        /// <summary>
        /// User Name
        /// </summary>
        /// <example>joe</example>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        /// <example>password</example>
        [Required]
        public string Password { get; set; }
    }
}
