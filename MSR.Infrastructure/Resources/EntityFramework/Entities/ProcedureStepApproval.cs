using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepApproval))]
    public partial class ProcedureStepApproval: TrackableEntity
    {
        public ProcedureStepApproval()
        {
            ProcedureStepDocumentApprovals = new HashSet<ProcedureStepDocumentApproval>();
            ProcedureStepMonitorApprovals = new HashSet<ProcedureStepMonitorApproval>();
        }

        public int ProcedureApprovalId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(4000)]
        public string StepText { get; set; }

        public int? GoToStepId { get; set; }

        public int PrintOrder { get; set; }

        [Column(TypeName = "money")]
        public decimal? ReplacementCost { get; set; }

        public float? Utilization { get; set; }

        public double? EquipmentTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Roles { get; set; }

        public virtual ProcedureApproval ProcedureApproval { get; set; }

        public virtual ICollection<ProcedureStepDocumentApproval> ProcedureStepDocumentApprovals { get; set; }

        public virtual ICollection<ProcedureStepMonitorApproval> ProcedureStepMonitorApprovals { get; set; }
    }
}
