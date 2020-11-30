namespace MSR.Domain.Models
{ 
    public class PartImportItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public string NickName { get; set; }
        public int MaximumCycles { get; set; }
    }
}