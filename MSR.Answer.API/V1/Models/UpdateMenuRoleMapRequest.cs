using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateMenuRoleMapRequest
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int MenuId { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanActivate { get; set; }
        public bool CanApprove { get; set; }
    }
}
