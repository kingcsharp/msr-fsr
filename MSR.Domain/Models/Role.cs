using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class Role
    {
        public Role()
        {
            Menus = new HashSet<MenuItem>();
        }

        public string Name { get; set; }

        public bool? IsCertificationRole { get; set; }

        public ICollection<MenuItem> Menus { get; set; }
    }
}
