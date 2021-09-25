namespace MSR.Domain.Models
{ 
    public class CycleCountHistoryImportItem
    {
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; }
        public int CycleCount { get; set; }
    }
}
