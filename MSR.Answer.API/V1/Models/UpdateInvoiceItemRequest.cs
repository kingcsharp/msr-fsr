using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateInvoiceItemRequest
    {
        [Required]
        public int? Id { get; set; }
    }
}
