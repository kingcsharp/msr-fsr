namespace Msr.Models.Invoices
{
    public class InvoiceWorkItem
    {
        public int Id { get; set; }
        public string ItemId { get; set; }
        public string InvoiceId { get; set; }
        public string RefPo { get; set; }
    }
}
