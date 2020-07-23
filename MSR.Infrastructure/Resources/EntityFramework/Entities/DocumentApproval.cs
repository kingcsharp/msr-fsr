using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(DocumentApproval))]
    public partial class DocumentApproval: ApprovalEntity
    {

        public int Revision { get; set; }

        public int RoleId { get; set; }

        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
    }
}
