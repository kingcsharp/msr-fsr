using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table("NCRHistory")]
    public class NCRHistoryItem: Entity
    {
        public int WorkOrderId { get; set; }
        public int WorkOrderTaskId { get; set; }
        public string Title { get; set; }
        public int WorkOrderPartId { get; set; }
        public int PartId { get; set; }
        public string SerialNumber { get; set; }
    }
}
