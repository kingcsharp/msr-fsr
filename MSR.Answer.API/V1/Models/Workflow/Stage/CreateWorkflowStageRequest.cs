using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models.Workflow
{
    public class CreateWorkflowStageRequest
    {
        public CreateWorkflowStageRequest()
        {
        }

        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
