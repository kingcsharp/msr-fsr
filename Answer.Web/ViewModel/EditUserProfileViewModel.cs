using System.ComponentModel.DataAnnotations;
using Msr.Models.Orders;

namespace Answer.Web.ViewModel
{
    public class UserProfileViewModel
    {
        public UserSummary UserSummary { get; set; }

        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}