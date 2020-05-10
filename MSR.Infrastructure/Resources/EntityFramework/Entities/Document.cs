using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Document))]
    public partial class Document: TrackableEntity
    {
        public Document()
        {
            DocumentApprovals = new HashSet<DocumentApproval>();
            DocumentEntityMaps = new HashSet<DocumentEntityMap>();
            ProcedureStepDocumentApprovals = new HashSet<ProcedureStepDocumentApproval>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int Revision { get; set; }

        public int RoleId { get; set; }

        public string Comments { get; set; }

        public virtual ICollection<DocumentApproval> DocumentApprovals { get; set; }

        public virtual ICollection<DocumentEntityMap> DocumentEntityMaps { get; set; }

        public virtual ICollection<ProcedureStepDocumentApproval> ProcedureStepDocumentApprovals { get; set; }
    }
}
