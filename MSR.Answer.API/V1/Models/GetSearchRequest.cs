using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class GetSearchRequest
    {
        [Required, MinLength(3)]
        public string SearchTerm { get; set; }
    }
}
