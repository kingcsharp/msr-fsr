using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrderPart))]
    public partial class WorkOrderPart: TrackableEntity
    {
        public WorkOrderPart()
        {
            Children = new HashSet<WorkOrderPart>();
        }

        public int WorkOrderId { get; set; }

        public int PartId { get; set; }

        public int? ParentId { get; set; }

        public int? CycleCount { get; set; }

        [StringLength(50)]
        public string SerialNumber { get; set; }

        public virtual Part Part { get; set; }

        public virtual WorkOrder WorkOrder { get; set; }
        public virtual ICollection<WorkOrderPart> Children { get; set; }
        public virtual WorkOrderPart Parent { get; set; }
    }
}
