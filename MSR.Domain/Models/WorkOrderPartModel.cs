using System.Collections.Generic;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Models
{
    public class WorkOrderPartModel
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int PartId { get; set; }
        public int CycleCount { get; set; }
        public int? ParentId { get; set; }
        public string SerialNumber { get; set; }
        public int? Qty { get; set; }
        public EnumSegregationType? SegregationType { get; set; }
        public virtual PartModel Part { get; set; }
        public virtual WorkOrderModel WorkOrder { get; set; }
        public virtual ICollection<WorkOrderPartModel> Children { get; set; }
        public virtual WorkOrderPartModel Parent { get; set; }
    }
}
