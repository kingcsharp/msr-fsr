namespace MSR.Domain.Models
{
    public class ProcedureStepTemplateModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StepText { get; set; }
        public double? LaborTime { get; set; }
        public double? ReplacementCost { get; set; }
        public double? Utilization { get; set; }
        public int? UsefulLife { get; set; }
    }
}
