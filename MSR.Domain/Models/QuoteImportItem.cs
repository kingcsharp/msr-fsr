using System;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Models
{
    public class QuoteImportItem
    {
        public int CustomerId { get; set; }
        public string DivisionFabNumber { get; set; }
        public int PartKitNumber { get; set; }
        public EnumSegregationType SegregationType { get; set; }
        public int ProcedureId { get; set; }
        public string ProductName { get; set; }
        public int Revision { get; set; }
        public decimal TotalPrice { get; set; }
        public int CycleTime { get; set; }
    }
}