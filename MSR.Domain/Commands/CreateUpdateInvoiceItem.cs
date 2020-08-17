namespace MSR.Domain.Commands
{
    public class CreateUpdateInvoiceItem
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int PurchaseOrderId { get; set; }
    }
}
