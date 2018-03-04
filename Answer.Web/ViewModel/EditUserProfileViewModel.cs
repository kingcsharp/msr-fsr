using System.Web.Mvc;
using Msr.Models.Orders;
using System.ComponentModel.DataAnnotations;

namespace Msr.Web.ViewModel
{
    public class UserProfileViewModel
    {
        public UserSummary UserSummary { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NedwPassword { get; set; }
    }
}