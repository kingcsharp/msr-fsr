using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class WorkflowGroupRoleMap : TrackableEntity
    {
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public int WorkflowGroupId { get; set; }
        public virtual WorkflowGroup WorkflowGroup { get; set; }
    }
}