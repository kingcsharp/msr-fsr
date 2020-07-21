using MSR.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateWorkflowStageRequest
    {
        public CreateWorkflowStageRequest()
        {
            WorkflowGroupStageMapModel = new List<WorkflowGroupStageMapModel>();
        }

        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<WorkflowGroupStageMapModel> WorkflowGroupStageMapModel { get; set; }

    }
}
