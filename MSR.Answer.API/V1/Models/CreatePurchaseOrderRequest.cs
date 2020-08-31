using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class CreatePurchaseOrderRequest
    {
        [Required]
        public int? CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ReferenceCustomerPO { get; set; }
        [Required]
        public string ReferenceName { get; set; }
        [Required]
        public int[] Products { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string CustomerReferenceNo { get; set; }
        public decimal TotalPurchaseLimit { get; set; }
        public decimal Tax { get; set; }
        
    }
}
