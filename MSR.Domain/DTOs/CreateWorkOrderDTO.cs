using System;
using System.Collections.Generic;
using System.Text;
using MSR.Domain.Commands;
using MSR.Domain.Models;

namespace MSR.Domain.DTOs
{
    public class CreateWorkOrderDTO
    {
        public int PurchaseId { get; set; }
        public int PurchaseOrderId { get; set; }
        public decimal Price { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ScheduledEndDate { get; set; }
        public bool HasNCR { get; set; }
        public int LocationId { get; set; }
        public virtual ICollection<WorkOrderPartModel> WorkOrderParts { get; set; }
        public virtual ICollection<WorkOrderTaskModel> WorkOrderTasks { get; set; }
        public ICollection<WorkOrderProduct> WorkOrderProducts { get; set; }

        public static CreateWorkOrderDTO FromCommand(CreateWorkOrder command)
        {
            return new CreateWorkOrderDTO()
            {
                PurchaseId = command.PurchaseId,
                PurchaseOrderId = command.PurchaseOrderId,
                ScheduledEndDate = command.ScheduledEndDate,
                ScheduledStartDate = command.ScheduledStartDate,
                HasNCR = command.HasNCR,
                LocationId = command.LocationId,
                WorkOrderProducts = command.WorkOrderProducts
            };
        }
    }
}
