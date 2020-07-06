using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models.Workflow
{
    public class UpdateWorkflowGroupRequest : CreateWorkflowGroupRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
