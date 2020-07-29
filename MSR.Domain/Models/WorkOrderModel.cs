using System;

namespace MSR.Domain.Models
{
    public class WorkOrderModel
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public bool HasNCR { get; set; }
        public int? LocationId { get; set; }
        public virtual LocationModel Location { get; set; }
        public virtual ProductModel Product { get; set; }
        /*
        public virtual Purchase Purchase { get; set; }
        public virtual ICollection<WorkOrderPart> WorkOrderParts { get; set; }
        public virtual ICollection<WorkOrderTask> WorkOrderTasks { get; set; }
        */
    }
}
