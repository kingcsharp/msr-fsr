using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateInvoiceRequest
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public int? Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal? TaxPercentage { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<UpdateInvoiceItemRequest> InvoiceItems { get; set; }
    }
}
