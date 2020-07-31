using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateInvoiceRequest
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string InvoiceClass { get; set; }

        public decimal? TaxPercentage { get; set; }

        [Required]
        public virtual ICollection<CreateInvoiceItemRequest> InvoiceItems { get; set; }
    }
}
