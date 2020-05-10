using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(TimeZone))]
    public partial class TimeZone: Entity
    {
        [StringLength(100)]
        public string Description { get; set; }

        public float? Offset { get; set; }

        public int? Number { get; set; }

        public short? UseDalightSavings { get; set; }
    }
}
