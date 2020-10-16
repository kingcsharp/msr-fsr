using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class GetPortalWorkOrderRequest
    {
        [Required]
        public int? CustomerId { get; set; }
        public string PartName { get; set; }
        public int? PartId { get; set; }
    }
}
