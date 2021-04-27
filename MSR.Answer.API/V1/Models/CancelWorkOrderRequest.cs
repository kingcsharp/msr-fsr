namespace MSR.Answer.API.V1.Models
{
    public class CancelWorkOrderRequest
    {
        public int WorkOrderId { get; set; }
        public bool Invoiceable { get; set; }
    }
}