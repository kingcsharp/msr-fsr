using MSR.Infrastructure.Resources.Services.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(HelpPage))]
    public class HelpPage: Entity
    {
        public string Title { get; set; }
        public string FriendlyUrl { get; set; }
        public string Content { get; set; }
        public virtual ICollection<HelpPageRoleMap> Roles { get; set; }
    }
}
