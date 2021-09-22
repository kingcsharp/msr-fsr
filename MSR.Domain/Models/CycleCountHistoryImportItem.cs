namespace MSR.Domain.Models
{ 
    public class CycleCountHistoryImportItem
    {
        public int? Id { get; set; }
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; }
        public int CycleCount { get; set; }
    }
}
