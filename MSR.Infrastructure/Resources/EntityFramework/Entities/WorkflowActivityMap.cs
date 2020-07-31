using System.ComponentModel.DataAnnotations.Schema;

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
