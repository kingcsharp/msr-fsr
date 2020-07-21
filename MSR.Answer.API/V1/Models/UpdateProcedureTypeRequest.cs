using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateProcedureTypeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
