using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class UpdateProcedureStep : Command
    {
        public int procedureStepId { get; set; }
        public int procedureId { get; set; }
        public string Title { get; set; }
        public string StepText { get; set; }
        public int? GoToStepId { get; set; }
        public double Duration { get; set; }
        public string DurationType { get; set; }
        public int PrintOrder { get; set; }
        public decimal? ReplacementCost { get; set; }
        public float? Utilization { get; set; }
        public double? EquipmentTime { get; set; }
        public List<Role> Roles { get; set; }
        public string procedureStepType { get; set; }
        public int? LaborTime { get; set; }
    }
}
