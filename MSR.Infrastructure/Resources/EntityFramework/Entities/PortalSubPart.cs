using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table("PortalSubParts")]
    public class PortalSubPart: Entity
    {
        public int WorkOrderId { get; set; }
        public int WorkOrderPartId { get; set; }
        public string SerialNumber { get; set; }
        public string PartNumber { get; set; }
        public int? CycleCount { get; set; }
        public int? Qty { get; set; }
        public string Name { get; set; }
    }
}
