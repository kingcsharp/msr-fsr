using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Reporting
{
   
    public class CombinedFinancialData
    {
        [Key]
        public long Id { get; set; }
        public string WONumber { get; set; }
        public string WOCreationDate { get; set; }
        public string DueDate { get; set; }
        public string ShipDate { get; set; }
        public string MSRFSRFacility { get; set; }
        public string CustomerName { get; set; }
        public string specno { get; set; }
        public string KitName { get; set; }
        public string pono { get; set; }
        public string mttn { get; set; }
        public string Amount { get; set; }
        public Single FillQty { get; set; }
        public string InvoiceId { get; set; }
        public string InvoiceDate { get; set; }
        public string SubTotal { get; set; }
        public string WTax { get; set; }
    }
}