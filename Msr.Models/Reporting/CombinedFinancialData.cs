using System;
using System.ComponentModel.DataAnnotations;

namespace Msr.Models.Reporting
{
   
    public class CombinedFinancialData
    {
        [Key]
        public long Id { get; set; }
        public string WONumber { get; set; }
        public string PONumber { get; set; }
        public DateTime WOCreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ShipDate { get; set; }
        public string MSRFSRFacility { get; set; }
        public string CustomerName { get; set; }
        public string SpecificationNumber { get; set; }
        public string KitName { get; set; }
        public string Serial { get; set; }
        public string CustomerPartNumber { get; set; }
        public string MTTN { get; set; }
        public string Amount { get; set; }
        public Single FillQty { get; set; }
        public string InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string SubTotal { get; set; }
        public string WTax { get; set; }
    }
}