using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class ProcedureStepTemplate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StepText { get; set; }
        public int? SystemTaskId { get; set; }
        public double? LaborTime { get; set; }
        public double? EquipmentTime { get; set; }
        public double? ReplacementCost { get; set; }
        public double? Utilization { get; set; }
        public int? UsefulLife { get; set; }
        public string Roles { get; set; }
        public string Comments { get; set; }
    }
}
