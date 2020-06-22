using MSR.Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class WorkflowGroupModel : EntityModel
    {
        public WorkflowGroupModel()
        {
            GroupRoles = new HashSet<WorkflowGroupRoleMapModel>();
        }

        public string Name { get; set; }

        public ICollection<WorkflowGroupRoleMapModel> GroupRoles { get; set; }
    }
}
