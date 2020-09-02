using MSR.Domain.Models.BaseModels;
using MSR.Domain.Views;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class PurchaseModel : CreatableModel
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderProductId { get; set; }
        public string CustomerPurchaseNumber { get; set; }
        public int LocationId { get; set; }
        public string SerialNumber { get; set; }
        public int Qty { get; set; }
        public int CustomerLineNumber { get; set; }
        public string MTTN { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public int StatusId { get; set; }
        public virtual StatusModel Status { get; set; }
        public virtual ICollection<WorkOrderModel> WorkOrders { get; set; }

        public virtual LocationModel Location { get; set; }
        public virtual PurchaseOrderModel PurchaseOrder { get; set; }
        public virtual PurchaseOrderProductView PurchaseOrderProduct { get; set; }
    }
}
