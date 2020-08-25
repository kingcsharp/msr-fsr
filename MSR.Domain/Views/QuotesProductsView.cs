using System;

namespace MSR.Domain.Views
{
    public class QuotesProductsView
    {
        public int Id { get; set; }
        public bool IsProduct { get; set; }
        public bool IsDeletable { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Company { get; set; }
        public string SubmittedBy { get; set; }
        public string PartKitNo { get; set; }
        public string ProcedureName { get; set; }

        public string ProductName { get; set; }
        public string Representative { get; set; }
        public int Revision { get; set; }
        public decimal EquipmentCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal SalesTax { get; set; }
        public decimal TotalPrice { get; set; }
        public int CycleTime { get; set; }
    }
}
