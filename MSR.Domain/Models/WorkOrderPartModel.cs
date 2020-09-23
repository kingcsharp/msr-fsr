using System.Collections.Generic;

namespace MSR.Domain.Models
{
    /// <summary>
    /// WorkOrderPartModel
    /// </summary>
    public class WorkOrderPartModel
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int PartId { get; set; }
        public int? ParentId { get; set; }
        public string SerialNumber { get; set; }
        public virtual PartModel Part { get; set; }
        public virtual WorkOrderModel WorkOrder { get; set; }
        public virtual ICollection<WorkOrderPartModel> Children { get; set; }
        public virtual WorkOrderPartModel Parent { get; set; }
    }
}
