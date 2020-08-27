using System;
using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    ///
    /// </summary>
    public class WorkOrderTaskRequest
    {
        /// <summary>
        ///
        /// </summary>
        public int WorkOrderId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ProcedureStepId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int ProcedureStepTypeId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int TaskStepOrder { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? AssignedTo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal? TotalTaskTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool? TaskIsRunning { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime? TaskRunningSince { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual ICollection<WorkOrderTaskMonitorRequest> WorkOrderTaskMonitors { get; set; }
    }
}
