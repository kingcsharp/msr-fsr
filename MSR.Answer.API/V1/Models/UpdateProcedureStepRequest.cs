using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateProcedureStepRequest
    {
        public int procedureStepId { get; set; }
        public string Title { get; set; }
        public string StepText { get; set; }
        public int? GoToStepId { get; set; }
        public double Duration { get; set; }
        public string DurationType { get; set; }
        public int PrintOrder { get; set; }
        public decimal? ReplacementCost { get; set; }
        public float? Utilization { get; set; }
        public double? EquipmentTime { get; set; }
        public string Roles { get; set; }
    }
}
