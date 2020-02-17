namespace Msr.Models.Menus
{
    public class MenuView
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public int Num { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Info { get; set; }
        public string GroupIcon { get; set; }
        public string GroupInfo { get; set; }
        public string GroupMenu { get; set; }
        public int OrderNumber { get; set; }
        public bool IsParent { get; set; }
    }
}
