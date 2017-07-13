namespace Msr.Models.Parts
{
    public class PartsView
    {
        public string Id { get; set; }
        public string ObjId { get; set; }
        public string Name { get; set; }
        public string CompanyPartNumber { get; set; }
        public string CompanyName { get; set; }
        public int? Revision { get; set; }
        public string Status { get; set; }
    }
}
