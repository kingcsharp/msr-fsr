namespace MSR.Answer.API.V1.Models
{
    public class UpdateWorkOrderPriceRequest
    {
        public int WorkOrderId { get; set; }
        public decimal Price { get; set; }
    }
}