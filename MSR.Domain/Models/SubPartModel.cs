namespace MSR.Domain.Models
{
    public class SubPartModel
    {
        public int? Id { get; set; }
        public int ParentId { get; set; }
        public int PartId { get; set; }
        public int Qty { get; set; }
        public int WorkOrderId { get; set; }
        public int WorkOrderPartId { get; set; }
        public string SerialNumber { get; set; }
        public string PartNumber { get; set; }
        public int? CycleCount { get; set; }
        public string Name { get; set; }
    }
}
