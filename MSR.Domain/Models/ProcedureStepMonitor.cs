using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class ProcedureStepMonitor
    {
        public ProcedureStepMonitor() { }
        public string Name { get; set; }
        public int ProcedureStepMonitorTypeId { get; set; }
        public int Revision { get; set; }
        public double Duration { get; set; }
        public string DurationType { get; set; }
    }
}
