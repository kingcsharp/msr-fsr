using System;

namespace Msr.Models.CustomerRequirements
{
    public class CustomerSubmittedRequirement
    {
        public int Id { get; set; }
        public string Respresentative { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
        public string Company { get; set; }
        public string Division { get; set; }
        public string PartKitNo { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedDate { get; set; }
        public int? Status { get; set; }
        public string QuoteJson { get; set; }
        public string CustomerRequirementJson { get; set; }
        public int? SupplierId { get; set; }
        public int? LocationId { get; set; }
        public string ProductName { get; set; }
        public int? PartId { get; set; }
        public int? ProcedureId { get; set; }
    }
}
