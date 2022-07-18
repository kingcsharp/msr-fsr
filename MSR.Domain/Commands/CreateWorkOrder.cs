using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateWorkOrder : Command
    {
        public int PurchaseId { get; set; }
        public ICollection<WorkOrderProduct> WorkOrderProducts { get; set; }
        public int PurchaseOrderId { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public bool HasNCR { get; set; }
        public int LocationId { get; set; }

        public CreateWorkOrder(int purchaseId, int purchaseOrderId,DateTime scheduledStartDate,
            DateTime scheduledEndDate, bool hasNcr, int locationId,ICollection<WorkOrderProduct> productRequest)
        {
            PurchaseId = purchaseId;
            PurchaseOrderId = purchaseOrderId;
            ScheduledEndDate = scheduledEndDate;
            ScheduledStartDate = scheduledStartDate;
            HasNCR = hasNcr;
            LocationId = locationId;
            WorkOrderProducts = productRequest;
        }

    }
}
