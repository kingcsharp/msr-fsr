using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public abstract class ApprovalEntity : TrackableEntity
    {
        public string Name { get; set; }
        public int WorkflowId { get; set; }
        [ForeignKey("WorkflowId")]
        public virtual Workflow Workflow { get; set; }
        public int WorkflowGroupId { get; set; }
        [ForeignKey("WorkflowGroupId")]
        public virtual WorkflowGroup WorkflowGroup { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User Created { get; set; }

        [NotMapped]
        public string ActivityType
        {
            get
            {
                return this.GetType().Name.Replace("Proxy","");
            }
        }
    }
}
