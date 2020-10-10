using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateWorkOrderTask : Command
    {
        public int WorkOrderId { get; set; }
        public int ProcedureStepId { get; set; }
        public int? ProcedureStepTypeId { get; set; }
        public int? StatusId { get; set; }
        public int TaskStepOrder { get; set; }
        public int? AssignedTo { get; set; }
        public decimal? TotalTaskTime { get; set; }
        public bool? TaskIsRunning { get; set; }
        public DateTime? TaskRunningSince { get; set; }
        public DateTime? StartedOn { get; set; }

        public virtual ICollection<int> WorkOrderTaskMonitorIds { get; set; }
    }
}
