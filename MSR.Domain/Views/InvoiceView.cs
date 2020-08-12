using MSR.Domain.Models.BaseModels;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Views
{
    public class InvoiceView
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// Invoice Total
        /// </summary>
        public decimal Amount { get; set; } // dbo.Invoice.Total
        /// <summary>
        /// Invoice Date
        /// </summary>
        public DateTime DueDate { get; set; } // dbo.Invoice.InvoiceDate
        public DateTime CreatedOn { get; set; }
        public string CreatedByName { get; set; } // dbo.Invoice.CreatedBy.GetFullName()
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedByName { get; set; }// dbo.Invoice.LastUpdatedBy.GetFullName() 
        public int StatusId { get; set; }
        public int LocationId { get; set; }
        public ICollection<InvoiceItemView> InvoiceItems { get; set; }
    }
}
