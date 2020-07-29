using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class WorkflowGroupStageMap : TrackableEntity
    {
        public int WorkflowStageId { get; set; }
        [ForeignKey("WorkflowStageId")]
        public virtual WorkflowStage WorkflowStage { get; set; }

        public int WorkflowGroupId { get; set; }
        [ForeignKey("WorkflowGroupId")]
        public virtual WorkflowGroup WorkflowGroup { get; set; }

    }
}