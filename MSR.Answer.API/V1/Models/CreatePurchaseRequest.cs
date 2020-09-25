using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    /// <summary>
    /// CreatePurchaseRequest
    /// </summary>
    public class CreatePurchaseRequest
    {

        /// <summary>
        /// PurchaseOrderId
        /// </summary>
        [Required]
        public int PurchaseOrderId { get; set; }

        /// <summary>
        /// Status ID
        /// </summary>
        [Required]
        public int StatusId { get; set; }

        /// <summary>
        /// PurchaseOrderProductId
        /// </summary>
        [Required]
        public int PurchaseOrderProductId { get; set; }

        /// <summary>
        /// CustomerPurchaseNumber
        /// </summary>
        public string CustomerPurchaseNumber { get; set; }

        /// <summary>
        /// LocationId
        /// </summary>
        [Required]
        public int LocationId { get; set; }

        /// <summary>
        /// SerialNumber
        /// </summary>
        [Required]
        [StringLength(20)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [Required]
        public int Qty { get; set; }

        /// <summary>
        /// CustomerLineNumber
        /// </summary>
        public int CustomerLineNumber { get; set; }

        /// <summary>
        /// Material Transfer Number (MTTN)
        /// </summary>
        [StringLength(10)]
        public string MTTN { get; set; }

        /// <summary>
        /// DueDate
        /// </summary>
        [Required]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// PurchasePrice
        /// </summary>
        [Required]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// If true, the quantity will be expanded out on the serialize step
        /// </summary>
        public bool SerializeIndividually { get; set; }
    }
}
