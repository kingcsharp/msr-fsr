using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models.Workflow
{
    public class UpdateWorkflowStageRequest : CreateWorkflowStageRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
