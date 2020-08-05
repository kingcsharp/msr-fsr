using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkflowGroup))]
    public partial class WorkflowGroup : DeletableEntity
    {
        public WorkflowGroup()
        {
            GroupRoles = new HashSet<WorkflowGroupRoleMap>();
            GroupUsers = new HashSet<WorkflowGroupUserMap>();
        }

        [StringLength(100)]
        public string Name { get; set; }
        public virtual ICollection<WorkflowGroupRoleMap> GroupRoles { get; set; }
        public virtual ICollection<WorkflowGroupUserMap> GroupUsers { get; set; }
    }
}
