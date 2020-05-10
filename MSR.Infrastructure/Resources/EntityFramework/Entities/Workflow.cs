using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(Workflow))]
    public partial class Workflow
    {
        [StringLength(100)]
        public string Name { get; set; }

        public bool? IsActive { get; set; }
    }
}
