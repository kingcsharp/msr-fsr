namespace Msr.Services.Orders.Procedures
{
    public class GetActualPartsShowHierarchy
    {
        public int Id { get; set; }
        public string Part_Desc { get; set; }
        public string Serial { get; set; }
        public int Tree_Level { get; set; }
        public int Object_Id { get; set; }
    }
}
