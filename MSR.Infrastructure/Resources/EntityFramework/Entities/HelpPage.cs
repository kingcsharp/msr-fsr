using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(HelpPage))]
    public class HelpPage: Entity
    {
        public string Title { get; set; }
        public string FriendlyUrl { get; set; }
        [Column("HelpContent")]
        public string Content { get; set; }
        public virtual ICollection<HelpPageRoleMap> Roles { get; set; }
    }
}
