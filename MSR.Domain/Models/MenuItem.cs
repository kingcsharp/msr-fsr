using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Models
{
    public class MenuItem
    {
        public string URL { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Icon { get; set; }
        public int OrderNumber { get; set; }
        public MenuGroup MenuGroup { get; set; }
        public int[] Permissions { get; set; }

        public EnumMenuItem EnumMenuItem { get; set; }

    }
}
