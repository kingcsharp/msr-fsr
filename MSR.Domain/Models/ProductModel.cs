
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public int Revision { get; set; }
        public int CustomerId { get; set; }
        public int? CustomerRequirementId { get; set; }
        public int ProcedureId { get; set; }
        public int PartId { get; set; }
        public decimal EquipmentCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal? SalesTax { get; set; }
        public decimal TotalSalePrice { get; set; }
        public int? CycleTime { get; set; }
        public virtual ICollection<WorkOrderModel> WorkOrders { get; set; }
    }
}
