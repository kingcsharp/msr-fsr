namespace MSR.Domain.Models
{
    public class ProcedureStepModel
    {
        public int Id { get; set; }
        public int ProcedureId { get; set; }
        public string Title { get; set; }
        public string StepText { get; set; }
        public int? GoToStepId { get; set; }
        public double Duration { get; set; }
        public string DurationType { get; set; }
        public int PrintOrder { get; set; }
        public double? LaborTime { get; set; }
        public decimal? ReplacementCost { get; set; }
        public float? Utilization { get; set; }
        public int? UsefulLife { get; set; }
        public double? EquipmentTime { get; set; }
        public string Roles { get; set; }
    }
}
