using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table("Role")]
    public partial class Role: TrackableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool? IsCertificationRole { get; set; }

        [StringLength(100)]
        public string OldId { get; set; }

    }
}
