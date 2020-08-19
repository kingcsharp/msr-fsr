
namespace MSR.Domain.Models
{
    
    public partial class InvoiceItemModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }

        public int PurchaseOrderId { get; set; }

        public int WorkOrderId { get; set; }

    }
}
