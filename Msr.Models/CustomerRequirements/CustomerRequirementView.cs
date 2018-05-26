using System;

namespace Msr.Models.CustomerRequirements
{
    public class CustomerRequirementView
    {
        public Guid Id { get; set; }
        public string Respresentative { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string Company { get; set; }
        public string Division { get; set; }
        public string PartKitNo { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Status { get; set; }
        public string SupplierId { get; set; }
        public string LocationId { get; set; }
        public string ProductName { get; set; }
        public string PartId { get; set; }
        public string ProcedureId { get; set; }
        public string ProductId { get; set; }
        public string ProductWorkflowId { get; set; }
        public string ProductStatus { get; set; }
        public string ProcedureName { get; set; }
        public string PObjectId { get; set; }
        public int CustomerSubmitId { get; set; }
        public Single? TotalSalePrice { get; set; }
        public Single? MaterialCost { get; set; }
        public string QuoteJson { get; set; }
        public string CustomerRequirementJson { get; set; }
        public double? LeadTime { get; set; }
        public int? Rev { get; set; }
    }
}
