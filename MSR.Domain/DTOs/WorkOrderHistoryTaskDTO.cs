using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.DTOs
{
    public class WorkOrderHistoryTaskDTO
    {
        public int StatusId { get; set; }
        public double? LaborTime { get; set; }
        public string ProcedureName { get; set; }
        public decimal? TotalTaskTime { get; set; }
        public int TaskStepOrder { get; set; }
        public string Title { get; set; }
        public int ProcedureStepTypeId { get; set; }
        public ICollection<string> WorkOrderTaskMonitors { get; set; }
    }
}
