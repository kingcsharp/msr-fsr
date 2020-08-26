using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreatePurchaseRequest
    {

        public int PurchaseOrderId { get; set; }

        public string CustomerPurchaseNumber { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        [StringLength(20)]
        public string SerialNumber { get; set; }

        [Required]
        public int Qty { get; set; }

        public int CustomerLineNumber { get; set; }

        [StringLength(10)]
        public string MTTN { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }
    }
}
