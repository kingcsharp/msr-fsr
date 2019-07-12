namespace Msr.Services.Orders.Procedures
{
    public class PurchaseItemInfoResult
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string CustLineItem { get; set; }
        public float Quantity { get; set; }
        public string CustomerWoItemNumber { get; set; }
        public string CompanyPartNumber { get; set; }
        public string Serial { get; set; }
        public string MaterialTransferTicketNumber { get; set; }
    }
}
