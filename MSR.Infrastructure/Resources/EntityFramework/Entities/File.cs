using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(File))]
    public partial class File: CreatableEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string FileURL { get; set; }

        [Required]
        [StringLength(100)]
        public string ContentType { get; set; }
    }
}
