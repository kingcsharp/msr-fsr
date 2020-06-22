using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands.Workflow
{
    public class CreateWorkflowGroupModel : Command
    {
        public CreateWorkflowGroupModel()
        {
            Roles = new List<WorkflowGroupRoleMapModel>() { };
        }
        
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<WorkflowGroupRoleMapModel> Roles { get; set; }
    }
}
