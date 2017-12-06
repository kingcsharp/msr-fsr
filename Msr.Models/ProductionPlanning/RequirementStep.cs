namespace Msr.Models.ProductionPlanning
{
    public class RequirementStep
    {
        public int Id { get; set; }
        public int CustomerSubmittedRequirementId { get; set; }
        public string ObjectId { get; set; }
        public string Process { get; set; }
        public int Step { get; set; }
        public decimal StandardDirectLaborMinutes { get; set; }
        public decimal StandardMachineMinutes { get; set; }
        public decimal? ReplacementCost { get; set; }
        public decimal? Utilization { get; set; }
        public decimal? UsefulLife { get; set; }
        public decimal? EquipExpensePerMinute { get; set; }
        public decimal? AnnualRm { get; set; }
        public decimal? RmPerMinute { get; set; }
    }
}
