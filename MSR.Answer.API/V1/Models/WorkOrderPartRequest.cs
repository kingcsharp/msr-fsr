using System.Collections.Generic;
using MSR.Domain.Commanding.Enums;

namespace MSR.Answer.API.V1.Models
{
    public class WorkOrderPartRequest
    {
        public int WorkOrderId { get; set; }
        public int PartId { get; set; }
        public int? ParentId { get; set; }
        public string SerialNumber { get; set; }
        public EnumSegregationType? SegregationType { get; set; }
        public virtual UpdatePartRequest Part { get; set; }
        public virtual UpdateWorkOrderRequest WorkOrder { get; set; }
        public virtual ICollection<WorkOrderPartRequest> Children { get; set; }
        public virtual WorkOrderPartRequest Parent { get; set; }
    }
}
