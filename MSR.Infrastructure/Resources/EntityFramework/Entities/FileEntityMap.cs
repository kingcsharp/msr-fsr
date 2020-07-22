using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(FileEntityMap))]
    public partial class FileEntityMap: CreatableEntity
    {

        public int FileId { get; set; }

        [Required]
        [StringLength(50)]
        public string EntityTableName { get; set; }

        public int EntityId { get; set; }
    }
}
