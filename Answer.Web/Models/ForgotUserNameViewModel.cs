using System.ComponentModel.DataAnnotations;

namespace Answer.Web.Models
{
    public class ForgotUserNameViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
    }
}