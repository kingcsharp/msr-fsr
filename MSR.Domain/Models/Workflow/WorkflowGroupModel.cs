using MSR.Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class WorkflowGroupModel
    {
        public WorkflowGroupModel()
        {
            GroupRoles = new HashSet<WorkflowGroupRoleMapModel>();
        }

        public string Name { get; set; }

        public DateTime? LastUpdatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string LastUpdatedByName { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        
        public int Id { get; set; }
        public bool IsActive { get; set; }

        public ICollection<WorkflowGroupRoleMapModel> GroupRoles { get; set; }
    }
}
