using System;
using System.Collections.Generic;

namespace Msr.Services.Orders.Procedures
{
    public class WipTaskResult
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Requestor { get; set; }

        public string CompletedBy { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualStopDate { get; set; }

        public string LatestRequesteeName { get; set; }
        public string ProcedureStepId { get; set; }

        public short HasFile { get; set; }

        public short HasMonitor { get; set; }

        public string Location { get; set; }

        public List<WipSubTaskResult> WipSubTasks { get; set; }
    }
}
