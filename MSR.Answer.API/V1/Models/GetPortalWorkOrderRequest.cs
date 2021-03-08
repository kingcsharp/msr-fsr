using MSR.Answer.API.V1.Models.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class GetPortalWorkOrderRequest: QueryRequestBase
    {
        [Required]
        public int CustomerId { get; set; }
        public string SubPartName { get; set; }
        public int? PartId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

    }
}
