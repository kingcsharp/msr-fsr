using System.Collections.Generic;

namespace MSR.Infrastructure.Resources.Answer.Models
{
    public class AnswerResponse
    {
        public string Object { get; set; }
        public string SuccessMessage { get; set; }
        public List<AnswerError> ErrorMessages { get; set; }
        public int Id { get; set; }
    }
}