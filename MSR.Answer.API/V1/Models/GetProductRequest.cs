using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class GetProductRequest
    {
        [Required]
        public int? Id { get; set; }
    }
}
