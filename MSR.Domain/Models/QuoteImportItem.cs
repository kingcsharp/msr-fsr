using System;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Models
{
    public class QuoteImportItem
    {
        public string CustomerName { get; set; }
        public string DivisionFab { get; set; }
        public string PartKitNo { get; set; }
        public int ProcedureId { get; set; }
        public string Name { get; set; }
        public int Revision { get; set; }
        public decimal TotalSalePrice { get; set; }
        public int CycleTime { get; set; }
        public int PartId { get; set; }
    }
}