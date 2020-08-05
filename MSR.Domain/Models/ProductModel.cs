using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class ProductModel : TrackableModel
    {
        public string Name { get; set; }
        public int Revision { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerRequirementId { get; set; }
        public int ProcedureId { get; set; }
        public virtual Procedure Procedure { get; set; }
        public int PartId { get; set; }
        public virtual PartModel Part { get; set; }
        public decimal EquipmentCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal? SalesTax { get; set; }
        public decimal TotalSalePrice { get; set; }
        public int CycleTime { get; set; }
    }
}
