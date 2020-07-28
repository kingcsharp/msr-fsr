using MSR.Domain.Models.BaseModels;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class InvoiceModel : TrackableModel
    {
        public string InvoiceNumber { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceClass { get; set; }
        public decimal Subtotal { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal Total { get; set; }
        public int StatusId { get; set; }
        public ICollection<InvoiceItemModel> InvoiceItems { get; set; }
    }
}
