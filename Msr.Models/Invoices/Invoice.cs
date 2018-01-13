using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Invoices
{
    public class Invoice
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Client { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string CustPo { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }
        
        public Double? TotalDue { get; set; }

        public Double? Tax { get; set; }

        public string Items { get; set; }

        public string Supplier { get; set; }

        [Required]
        public string InvoiceClass { get; set; }
    }
}
