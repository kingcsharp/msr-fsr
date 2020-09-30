using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStep))]
    public partial class ProcedureStep: TrackableEntity
    {
        public ProcedureStep()
        {
            ProcedureStepMonitors = new HashSet<ProcedureStepMonitor>();
            WorkOrderTasks = new HashSet<WorkOrderTask>();
        }

        public int ProcedureId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(4000)]
        public string StepText { get; set; }

        public int? GoToStepId { get; set; }

        public int PrintOrder { get; set; }
        public double? LaborTime { get; set; }
        public double? EquipmentTime { get; set; }

        [ForeignKey("ProcedureStepTypeId")]
        public ProcedureStepType StepType { get; set; }
        public int? ProcedureStepTypeId { get; set; }

        [Column(TypeName = "money")]
        public decimal? ReplacementCost { get; set; }

        public float? Utilization { get; set; }

        public int? UsefulLife { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<ProcedureStepMonitor> ProcedureStepMonitors { get; set; }

        public virtual ICollection<WorkOrderTask> WorkOrderTasks { get; set; }

        public virtual ICollection<ProcedureStepRoleMap> ProcedureStepRoles { get; set; }
    }
}
