using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class CreateWorkOrderRequest
    {
        [Required]
        public int? PurchaseId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public bool? HasNCR { get; set; }
        public int? LocationId { get; set; }
        public bool SerializeIndividually { get; set; }
        public virtual ICollection<WorkOrderPartRequest> WorkOrderParts { get; set; }
        public virtual ICollection<WorkOrderTaskRequest> WorkOrderTasks { get; set; }
        public ICollection<string> SerialNumbers { get; set; }
        public ICollection<string> CustomerLineNumbers { get; set; }
    }
}
