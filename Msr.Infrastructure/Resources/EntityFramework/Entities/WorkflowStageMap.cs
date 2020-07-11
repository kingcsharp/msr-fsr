using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
