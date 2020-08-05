using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderProductId { get; set; }
        public string CustomerPurchaseNumber { get; set; }
        public int LocationId { get; set; }
        public int Qty { get; set; }
        public decimal PurchasePrice { get; set; }
        public virtual ICollection<WorkOrderModel> WorkOrders { get; set; }
    }
}
