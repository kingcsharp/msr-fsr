using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class ProductDownloadView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Revision { get; set; }
        public int CustomerId { get; set; }
        public int ProcedureId { get; set; }
        public int PartId { get; set; }
        public decimal? LaborCost { get; set; }
        public decimal? EquipmentCost { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal? SalesTax { get; set; }
        public decimal TotalSalePrice { get; set; }
        public int CycleTime { get; set; }
        public int? QuoteId { get; set; }
        public string DivisionFab { get; set; }
        public int CustomerRequirementId { get; set; }
    }
}
