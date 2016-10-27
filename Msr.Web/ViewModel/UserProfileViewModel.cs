using System.ComponentModel.DataAnnotations;

namespace Msr.Web.ViewModel
{
    public class EditUserProfileViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Phone2 { get; set; }
    }
}