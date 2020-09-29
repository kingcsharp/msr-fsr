using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(DocumentRoleMap))]
    public class DocumentRoleMap : CreatableEntity
    {
        [Required]
        public int DocumentId { get; set; }
        
        [Required]
        public int RoleId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
