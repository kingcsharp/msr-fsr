using System;

namespace Msr.Services.Quotes.ViewModels
{
    public class SaveProductRequest
    {
        public int Id { get; set; }
        public int? CycleTime { get; set; }
        public string NtLogin { get; set; }
        public string PObjectId { get; set; }
        public Single? MaterialCost { get; set; }
        public int CustomerRequirementId { get; set; }
        public bool IsProduct { get; set; }
        public Single? TotalSalePrice { get; set; }
        public string CustomerId { get; set; }
        public string Division { get; set; }
        public string SupplierId { get; set; }
        public string LocationId { get; set; }
        public string ProductName { get; set; }
        public string PartId { get; set; }
        public string ProcedureId { get; set; }
        public double? LeadTime { get; set; }
        public double? Price { get; set; }
    }
}