using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class NCRPartWODetailView
    {
        public int WorkOrderId { get; set; }
        public DateTime? WorkOrderCompletedDate { get; set; }
        public int WorkOrderPartId { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string SerialNumber { get; set; }
        public List<NCRPartWOTaskView> WorkOrderTasks { get; set; }
    }

    public class NCRPartWOTaskView
    {
        public string WorkOrderTaskTitle { get; set; }
        public int WorkOrderTaskId { get; set; }
        public List<WorkOrderPartMonitorView> WorkOrderTaskMonitors { get; set; }
        public List<FileModel> Files { get; set; }
        public List<DocumentView> Documents { get; set; }
    }
}
