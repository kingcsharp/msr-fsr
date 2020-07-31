using System;
using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class WorkOrderTaskRequest
    {
        public int WorkOrderId { get; set; }
        public int ProcedureStepId { get; set; }
        public int ProcedureStepTypeId { get; set; }
        public int StatusId { get; set; }
        public int TaskStepOrder { get; set; }
        public int? AssignedTo { get; set; }
        public int? UserId { get; set; }
        public decimal? TotalTaskTime { get; set; }
        public bool? TaskIsRunning { get; set; }
        public DateTime? TaskRunningSince { get; set; }
        public virtual ICollection<WorkOrderTaskMonitorRequest> WorkOrderTaskMonitors { get; set; }
    }
}
