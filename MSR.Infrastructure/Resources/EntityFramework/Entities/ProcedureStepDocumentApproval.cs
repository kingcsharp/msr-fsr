using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(ProcedureStepDocumentApproval))]
    public partial class ProcedureStepDocumentApproval:TrackableEntity
    {
        public int ProcedureStepApprovalId { get; set; }

        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual ProcedureStepApproval ProcedureStepApproval { get; set; }

    }
}
