using System;

namespace Msr.Models.Invoices
{
    public class InvoiceView
    {
        public int? Id { get; set; }

        public string Client { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string CustPo { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? Total { get; set; }

        public decimal? Tax { get; set; }

        public string Items { get; set; }

        public string Supplier { get; set; }

        public string InvoiceClass { get; set; }
    }
}
