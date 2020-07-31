using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateWorkOrder : Command
    {
        public int? PurchaseId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public bool? HasNCR { get; set; }
        public int? LocationId { get; set; }
        public virtual ICollection<WorkOrderPartModel> WorkOrderParts { get; set; }
        public virtual ICollection<WorkOrderTaskModel> WorkOrderTasks { get; set; }

    }
}
