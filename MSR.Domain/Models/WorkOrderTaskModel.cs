using System;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class WorkOrderTaskModel
    {
        public int WorkOrderId { get; set; }
        public int ProcedureStepId { get; set; }
        public int ProcedureStepTypeId { get; set; }
        public int StatusId { get; set; }
        public int TaskStepOrder { get; set; }
        public int? AssignedTo { get; set; }
        public virtual UserModel AssignedToUser { get; set; }
        public decimal? TotalTaskTime { get; set; }
        public bool? TaskIsRunning { get; set; }
        public DateTime? TaskRunningSince { get; set; }
        public virtual ProcedureStepModel ProcedureStep { get; set; }
        public virtual ProcedureStepTypeModel ProcedureStepType { get; set; }
        public virtual StatusModel Status { get; set; }
        public virtual WorkOrderModel WorkOrder { get; set; }
        public virtual ICollection<WorkOrderTaskMonitorModel> WorkOrderTaskMonitors { get; set; }

        /// <summary>
        /// This needs to be added
        /// </summary>
        /// <value>This needs to be added</value>
        [DataMember(Name="referenceFiles")]
        public List<FileModel> ReferenceFiles { get; set; }
    }
}
