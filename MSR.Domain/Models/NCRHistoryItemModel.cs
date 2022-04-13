using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class NCRHistoryItemModel
    {
        public int WorkOrderId { get; set; }
        public int WorkOrderTaskId { get; set; }
        public string Title { get; set; }
        public int WorkOrderPartId { get; set; }
        public int PartId { get; set; }
        public string SerialNumber { get; set; }
    }
}
