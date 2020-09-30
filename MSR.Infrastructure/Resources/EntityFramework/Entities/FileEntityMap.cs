using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(FileEntityMap))]
    public partial class FileEntityMap : CreatableEntity
    {
        public int FileId { get; set; }

        [ForeignKey("FileId")]
        public virtual File FileObject { get; set; }

        [Required]
        [StringLength(50)]
        public string EntityTableName { get; set; }

        public int EntityId { get; set; }

        public ProcedureStepTemplate ProcedureTemplate { get; set; }
        public Procedure Procedure { get; set; }
        // Procedure
        // Part
        // Anything else files can be attached to
        // etc.
    }
}
