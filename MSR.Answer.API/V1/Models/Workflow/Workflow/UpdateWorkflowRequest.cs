using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models.Workflow
{
    public class UpdateWorkflowRequest : CreateWorkflowRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
