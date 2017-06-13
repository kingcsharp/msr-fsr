using System.ComponentModel.DataAnnotations;

namespace Msr.Web.ViewModel.Help
{
    public class SupportViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Details { get; set; }

        public string ContactMathod { get; set; }
    }
}