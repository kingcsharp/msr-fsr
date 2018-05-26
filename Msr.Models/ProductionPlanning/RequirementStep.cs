using System;

namespace Msr.Models.ProductionPlanning
{
    public class RequirementStep
    {
        public int Id { get; set; }
        public int CustomerSubmittedRequirementId { get; set; }
        public string ObjectId { get; set; }
        public string Process { get; set; }
        public int Step { get; set; }
    }
}
