using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrder))]
    public partial class WorkOrder: TrackableEntity
    {
        public WorkOrder()
        {
            WorkOrderParts = new HashSet<WorkOrderPart>();
            WorkOrderTasks = new HashSet<WorkOrderTask>();
        }

        public int PurchaseId { get; set; }

        public int ProductId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public DateTime ScheduledStartDate { get; set; }

        public DateTime ScheduledEndDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        public bool HasNCR { get; set; }

        public int? LocationId { get; set; }

        [ForeignKey("LocationId")]

        public virtual Location Location { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }

        public virtual ICollection<WorkOrderPart> WorkOrderParts { get; set; }

        public virtual ICollection<WorkOrderTask> WorkOrderTasks { get; set; }
    }
}
