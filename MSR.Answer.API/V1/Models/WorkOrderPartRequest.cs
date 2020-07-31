using MSR.Domain.Commands;
using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class WorkOrderPartRequest
    {
        public int WorkOrderId { get; set; }
        public int PartId { get; set; }
        public int? ParentId { get; set; }
        public string SerialNumber { get; set; }
        public virtual UpdatePartRequest Part { get; set; }
        public virtual UpdateWorkOrderRequest WorkOrder { get; set; }
        public virtual ICollection<WorkOrderPartRequest> Children { get; set; }
        public virtual WorkOrderPartRequest Parent { get; set; }
    }
}
