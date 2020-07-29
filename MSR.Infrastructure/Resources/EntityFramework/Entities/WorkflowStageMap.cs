using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkflowStageMap))]
    public class WorkflowStageMap : TrackableEntity
    {
        public int WorkflowId { get; set; }
        [ForeignKey("WorkflowId")]
        public virtual Workflow Workflow { get; set; }
        public int WorkflowStageId { get; set; }
        [ForeignKey("WorkflowStageId")]
        public virtual WorkflowStage WorkflowStage { get; set; }
    }
}
