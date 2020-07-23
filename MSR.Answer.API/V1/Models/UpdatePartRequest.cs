using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdatePartRequest : CreatePartRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
