using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class GetSearchRequest
    {
        [Required]
        public string SearchTerm { get; set; }
    }
}
