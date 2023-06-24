using System;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class WorkOrderModel
    {
        public int? Id { get; set; }
        public string CustomerName { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public string? ScheduledEndDateChangeReason { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public bool? HasNCR { get; set; }
        public int? LocationId { get; set; }
        public string Status { get; set; }
        public LocationModel Location { get; set; }
        public int? ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int? PurchaseId { get; set; }
        public PurchaseModel Purchase { get; set; }
        public ICollection<WorkOrderPartModel> WorkOrderParts { get; set; }
        public ICollection<WorkOrderTaskModel> WorkOrderTasks { get; set; }
        public ICollection<WorkOrderMessageModel> WorkOrderMessages { get; set; }
        public bool HasSubParts { get; set; }
        public bool? CustomerLastRespondent { get; set; }
        public ICollection<ProductModel> WorkOrderProducts { get; set; }
        public bool? Invoiceable { get; set; }
    }
}
