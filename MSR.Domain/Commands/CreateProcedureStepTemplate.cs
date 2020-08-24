using System;
using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateProcedureStepTemplate : Command
    {
        public string Title { get; set; }
        public string StepText { get; set; }
        public int? SystemTaskId { get; set; }
        public decimal? LaborTime { get; set; }
        public decimal? EquipmentTime { get; set; }
        public decimal? ReplacementCost { get; set; }
        public Single? Utilization { get; set; }
        public int? UsefulLife { get; set; }
        public string Roles { get; set; }
        public string Comments { get; set; }
    }
}
