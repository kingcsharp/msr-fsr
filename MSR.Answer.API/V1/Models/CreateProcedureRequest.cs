using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateProcedureRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int ProcedureTypeId { get; set; }
        public int Revision { get; set; }
        public double Duration { get; set; }
        [Required]
        [StringLength(20)]
        public string DurationType { get; set; }
    }
}
