using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(MonitorType))]
    public partial class MonitorType : Entity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
