using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Enums;
using System;

namespace MSR.Domain.Commands
{
    public class GetQuotesProducts : PagingCommand
    {
        public DateTime? SubmittedDate { get; set; }
        public string Company { get; set; }
        public string SubmittedByFullName { get; set; }
        public string DivisionFab { get; set; }
        public string PartKitNo { get; set; }
        public EnumSegregationType[]? SegregationType { get; set; }
        public int? ProcedureId { get; set; }
        public string ProcedureName { get; set; }
        public string ProductName { get; set; }
        public string Representative { get; set; }
        public int? Revision { get; set; }
        public decimal? EquipmentCost { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal? SalesTax { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? CycleTime { get; set; }
        public DateTime? LastUpdateOn { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
