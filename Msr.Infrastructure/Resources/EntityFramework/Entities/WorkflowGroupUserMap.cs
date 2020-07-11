using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkflowGroupUserMap))]
    public class WorkflowGroupUserMap : TrackableEntity
    {
        public int UserId { get; set; }
        public int WorkflowGroupId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("WorkflowGroupId")]
        public virtual WorkflowGroup WorkflowGroup { get; set; }
    }
}
