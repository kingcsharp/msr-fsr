using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureApproval))]
    public partial class ProcedureApproval: ApprovalEntity
    {
        public ProcedureApproval()
        {
            ProcedureStepApprovals = new HashSet<ProcedureStepApproval>();
        }

        public int ProcedureId { get; set; }

        public int ProcedureTypeId { get; set; }

        public int Revision { get; set; }

        public double Duration { get; set; }

        [Required]
        [StringLength(20)]
        public string DurationType { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual ProcedureType ProcedureType { get; set; }

        public virtual ICollection<ProcedureStepApproval> ProcedureStepApprovals { get; set; }
    }
}
