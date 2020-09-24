using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(DocumentApproval))]
    public partial class DocumentApproval: ApprovalEntity
    {
        [Required]
        public int Revision { get; set; }

        public int? DocumentId { get; set; }

        public string Comments { get; set; }

        public string ApprovalJSON { get; set; }

        public virtual Document Document { get; set; }
    }
}
