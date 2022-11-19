using System;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Models
{
    public class QuoteImportItem
    {
        public string CustomerName { get; set; }
        public string DivisionFab { get; set; }
        public int PartKitNo { get; set; }
        public EnumSegregationType SegregationType { get; set; }
        public int ProcedureId { get; set; }
        public string ProductName { get; set; }
        public int Revision { get; set; }
        public decimal TotalSalesPrice { get; set; }
        public int CycleTime { get; set; }
    }
}