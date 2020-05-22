using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(DocumentEntityMap))]
    public partial class DocumentEntityMap: TrackableEntity
    {
        public int DocumentId { get; set; }

        [Required]
        [StringLength(50)]
        public string EntityTableName { get; set; }

        public int EntityId { get; set; }
        public virtual Document Document { get; set; }
    }
}
