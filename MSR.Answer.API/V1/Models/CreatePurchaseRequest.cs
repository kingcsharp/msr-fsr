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
        [Required]
        ICollection<PurchaseRequest> PurchaseRequests { get; set; }

        [Required]
        public bool GroupLines { get; set; }
    }
}
