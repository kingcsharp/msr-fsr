
namespace MSR.Domain.Views
{
    
    public class InvoiceItemView
    {
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }
        public string PurchaseNumber { get; set; }

        public int WorkOrderId { get; set; }

    }
}
