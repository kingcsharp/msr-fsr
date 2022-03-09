using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table("WOSubParts")]
    public class SubPart: Entity
    {
        public int WorkOrderId { get; set; }
        public int WorkOrderPartId { get; set; }
        public string SerialNumber { get; set; }
        public string PartNumber { get; set; }
        public int? Qty { get; set; }
        public string Name { get; set; }
        public int PartId { get; set; }
        public int ParentId { get; set; }
    }
}
