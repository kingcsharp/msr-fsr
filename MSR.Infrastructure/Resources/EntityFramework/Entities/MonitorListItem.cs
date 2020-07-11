using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(MonitorListItem))]
    public partial class MonitorListItem : Entity
    {
        [StringLength(20)]
        public string Name { get; set; }

        public int MonitorListId { get; set; }

        public virtual MonitorList List { get; set; }
    }
}
