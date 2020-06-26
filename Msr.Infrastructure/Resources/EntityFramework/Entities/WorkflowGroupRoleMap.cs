using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class WorkflowGroupRoleMap : TrackableEntity
    {
        public int RoleId { get; set; }
        public int WorkflowGroupId { get; set; }
        public virtual WorkflowGroup WorkflowGroup { get; set; }
    }
}
