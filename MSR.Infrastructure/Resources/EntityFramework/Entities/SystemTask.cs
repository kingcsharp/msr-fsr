using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(SystemTask))]
    public partial class SystemTask: Entity
    {
        public SystemTask()
        {
            ProcedureSteps = new HashSet<ProcedureStep>();
            ProcedureStepApprovals = new HashSet<ProcedureStepApproval>();
            WorkOrderTasks = new HashSet<WorkOrderTask>();
        }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<ProcedureStep> ProcedureSteps { get; set; }

        public virtual ICollection<ProcedureStepApproval> ProcedureStepApprovals { get; set; }

        public virtual ICollection<WorkOrderTask> WorkOrderTasks { get; set; }
    }
}
