using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateWorkflowGroupModel : Command
    {
        public CreateWorkflowGroupModel()
        {
            Roles = new HashSet<WorkflowGroupRoleMapModel>() { };
            Users = new HashSet<WorkflowGroupUserMapModel>() { };
        }
        
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<WorkflowGroupRoleMapModel> Roles { get; set; }
        public ICollection<WorkflowGroupUserMapModel> Users { get; set; }
    }
}
