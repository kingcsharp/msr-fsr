namespace MSR.Domain.Hub
{
    public class WorkOrderStatusUpdate {
        public int workOrderId { get; set; }
        public string workOrderStatus { get; set; }
        public string productName { get; set; }
        public string partNumber { get; set; }
        public string procedureName { get; set; }
        public string locationName { get; set; }
        public string customerName { get; set; }
        public string serialNumber { get; set; }
    }
}
