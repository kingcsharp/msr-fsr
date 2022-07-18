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
        public bool GroupLines { get; set; }
        public List<PurchaseRequest> PurchaseRequests { get; set; }
    }
}
