using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateWorkOrderRequest
    {
        [Required]
        public int PurchaseId { get; set; }
        public ICollection<WorkOrderProductRequest> WorkOrderProducts { get; set; }
        public int PurchaseOrderId { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public bool HasNCR { get; set; }
        public int LocationId { get; set; }

    }
}
