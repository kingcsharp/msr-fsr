using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateInvoiceRequest
    {
        [Required]
        public int? Id { get; set; }

        public string Description { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal? TaxPercentage { get; set; }

        public ICollection<UpdateInvoiceItemRequest> InvoiceItems { get; set; }
    }
}
