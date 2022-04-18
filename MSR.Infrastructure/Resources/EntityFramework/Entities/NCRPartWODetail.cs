using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    public class NCRPartWODetail
    {
        public int WorkOrderId { get; set; }
        public DateTime? WorkOrderCompletedDate { get; set; }
        public int WorkOrderPartId { get; set; }
        public string WorkOrderTaskTitle { get; set; }
        public int WorkOrderTaskId { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string SerialNumber { get; set; }
        public int WorkOrderTaskMonitorId { get; set; }
        public string WorkOrderTaskMonitorDescription { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
    }
}
