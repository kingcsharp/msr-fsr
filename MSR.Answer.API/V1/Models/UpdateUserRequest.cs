using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateUserRequest: CreateUserRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
