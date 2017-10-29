namespace Msr.Models.ProductionPlanning
{
    public class RequirementStepsView
    {
        public int Id { get; set; }
        public string ObjectId { get; set; }
        public string Process { get; set; }
        public int Step { get; set; }
        public decimal StandardDirectLaborMinutes { get; set; }
        public decimal StandardMachineMinutes { get; set; }
        public decimal ReplacementCost { get; set; }
        public decimal Utilization { get; set; }
        public decimal UsefulLife { get; set; }
        public decimal EquipExpensePerMinute { get; set; }
        public decimal AnnualRM { get; set; }
        public decimal RMPerMinute { get; set; }
    }
}
