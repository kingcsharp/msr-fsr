using MSR.Domain.Commanding.Enums;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class MenuItem
    {
        public MenuItem()
        {
            Roles = new HashSet<Role>();
        }

        public string URL { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Icon { get; set; }
        public int OrderNumber { get; set; }
        public MenuGroup MenuGroup { get; set; }
        public int[] Permissions { get; set; }
        public ICollection<Role> Roles { get; set; }

        public EnumMenuItem EnumMenuItem { get; set; }

    }
}
