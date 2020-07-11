using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(MonitorList))]
    public partial class MonitorList : Entity
    {
        [StringLength(20)]
        public string Name { get; set; }
    }
}
