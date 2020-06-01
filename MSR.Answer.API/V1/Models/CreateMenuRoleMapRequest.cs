using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateMenuRoleMapRequest
    {
        [Required]
        public int MenuId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
