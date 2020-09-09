namespace MSR.Answer.API.V1.Models
{
    public class CreateQuoteItemRequest
    {
        public string ItemNo { get; set; }
        public int? Qty { get; set; }
        public string Description { get; set; }
        public string LeadTime { get; set; }
        public string CustomerPartNo { get; set; }
        public decimal? Price { get; set; }
        public decimal? Extension { get; set; }
    }
}
