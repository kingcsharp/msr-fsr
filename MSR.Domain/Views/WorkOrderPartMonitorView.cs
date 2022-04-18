using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class WorkOrderPartMonitorView
    {
        public int WorkOrderTaskMonitorId { get; set; }
        public string WorkOrderTaskMonitorDescription { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
    }
}
