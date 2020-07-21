using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Procedure))]
    public partial class Procedure: TrackableEntity
    {
        public Procedure()
        {
            ProcedureApprovals = new HashSet<ProcedureApproval>();
            ProcedureSteps = new HashSet<ProcedureStep>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int ProcedureTypeId { get; set; }

        public int Revision { get; set; }

        public double Duration { get; set; }

        [Required]
        [StringLength(20)]
        public string DurationType { get; set; }

        public virtual ICollection<ProcedureApproval> ProcedureApprovals { get; set; }

        public virtual ProcedureType ProcedureType { get; set; }

        public virtual ICollection<ProcedureStep> ProcedureSteps { get; set; }
    }
}
