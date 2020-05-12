using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(DocumentApproval))]
    public partial class DocumentApproval: TrackableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int Revision { get; set; }

        public int RoleId { get; set; }

        public string Comments { get; set; }

        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
