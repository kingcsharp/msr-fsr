using System;
using System.Collections.Generic;
using MSR.Domain.Commanding;
using MSR.Domain.Models;

namespace MSR.Domain.Commands
{
    public class UpdateWorkOrder: Command
    {
        public int Id { get; set; }
        public int? PurchaseId { get; set; }
        public int? ProductId { get; set; }
        public int? PurchaseOrderId { get; set; }
        public decimal? Price { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public bool? HasNCR { get; set; }
        public int? LocationId { get; set; }
        public int Qty { get; set; }
        public bool SerializeIndividually { get; set; }
        public ICollection<string> SerialNumbers { get; set; }
        public ICollection<string> CustomerLineNumbers { get; set; }
        public virtual ICollection<WorkOrderPartModel> WorkOrderParts { get; set; }
        public virtual ICollection<WorkOrderTaskModel> WorkOrderTasks { get; set; }
    }
}
