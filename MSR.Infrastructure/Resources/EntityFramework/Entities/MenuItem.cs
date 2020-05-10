using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(MenuItem))]
    public partial class MenuItem : TrackableEntity
    {
        [Required]
        [StringLength(255)]
        public string URL { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Info { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public int OrderNumber { get; set; }

        public int MenuGroupId { get; set; }
        public virtual MenuGroup MenuGroup { get; set; }

        public ICollection<MenuRole> Roles { get; set;}
    }
}
