using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class WorkflowActivityMap : TrackableEntity
    {
        public int WorkflowId { get; set; }
        [ForeignKey("WorkflowId")]
        public virtual Workflow Workflow { get; set; }
        public int WorkflowActivityId { get; set; }
        [ForeignKey("WorkflowActivityId")]
        public virtual WorkflowActivity WorkflowActivity { get; set; }
    }
}
