using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(WorkOrderTask))]
    public partial class WorkOrderTask: TrackableEntity
    {
        public WorkOrderTask()
        {
            WorkOrderTaskMonitors = new HashSet<WorkOrderTaskMonitor>();
        }

        public int WorkOrderId { get; set; }

        public int ProcedureStepId { get; set; }

        public int StatusId { get; set; }

        public int TaskStepOrder { get; set; }

        public int? AssignedTo { get; set; }

        [ForeignKey("AssignedTo")]
        public virtual User AssignedToUser { get; set; }

        public decimal? TotalTaskTime { get; set; }

        public bool? TaskIsRunning { get; set; }

        public DateTime? TaskRunningSince { get; set; }

        public virtual ProcedureStep ProcedureStep { get; set; }

        public virtual Status Status { get; set; }

        public virtual WorkOrder WorkOrder { get; set; }

        public virtual ICollection<WorkOrderTaskMonitor> WorkOrderTaskMonitors { get; set; }
    }
}
