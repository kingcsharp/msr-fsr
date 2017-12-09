using System;

namespace Msr.Models.CustomerRequirements
{
    public class CustomerRequirementView
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
        public string Status { get; set; }
        public string QuoteJson { get; set; }
        public string CustomerRequirementJson { get; set; }
        public string SupplierId { get; set; }
        public string LocationId { get; set; }
        public string ProductName { get; set; }
        public string PartId { get; set; }
        public string ProcedureId { get; set; }
        public string ProductId { get; set; }
        public string ProductStatus { get; set; }
    }
}
