using MSR.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateWorkflowRequest
    {
        public CreateWorkflowRequest()
        {
            MemberStages = new HashSet<WorkflowStageMapModel>() { };
            ActivityMaps = new HashSet<WorkflowActivityMapModel>() { };
        }

        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<WorkflowStageMapModel> MemberStages { get; set; }
        public ICollection<WorkflowActivityMapModel> ActivityMaps { get; set; }
    }
}
