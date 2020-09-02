using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(MonitorInputType))]
    public partial class MonitorInputType : Entity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int MonitorTypeId { get; set; }

        [ForeignKey("MonitorTypeId")]
        public virtual MonitorType Type { get; set; }
    }
}
