using MSR.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models.Workflow
{
    public class CreateWorkflowGroupRequest
    {
        public CreateWorkflowGroupRequest()
        {
            Roles = new HashSet<WorkflowGroupRoleMapModel>() { };
            Users = new HashSet<WorkflowGroupUserMapModel>() { };
        }

        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<WorkflowGroupRoleMapModel> Roles { get; set; }
        public ICollection<WorkflowGroupUserMapModel> Users { get; set; }

    }
}
