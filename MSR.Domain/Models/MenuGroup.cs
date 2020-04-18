using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class MenuGroup
    {
        public string URL { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Icon { get; set; }
        public int OrderNumber { get; set; }
        ICollection<MenuItem> MenuItems { get; set; }
    }
}
