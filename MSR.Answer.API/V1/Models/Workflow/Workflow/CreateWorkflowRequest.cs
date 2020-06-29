using MSR.Domain.Models;
using MSR.Domain.Models.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models.Workflow
{
    public class CreateWorkflowRequest
    {
        public CreateWorkflowRequest()
        {
            MemberStages = new List<WorkflowStageMapModel>() { };
            ActivityMaps = new List<WorkflowActivityMapModel>() { };
        }

        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<WorkflowStageMapModel> MemberStages { get; set; }
        public ICollection<WorkflowActivityMapModel> ActivityMaps { get; set; }
    }
}
