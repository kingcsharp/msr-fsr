using System;
using System.Collections.Generic;
using System.Text;
using MSR.Domain.Commands;
using MSR.Domain.Events;

namespace MSR.Domain.Extensions
{
    public static class EventMappingExtensions
    {
        public static CreateWorkOrder ToCreateWorkOrderCommand(this WorkOrderCreateEvent createEvent)
        {
            return new CreateWorkOrder(createEvent.PurchaseId, createEvent.ProductId, createEvent.PurchaseOrderId,
                createEvent.Price, createEvent.ScheduledStartDate,
                createEvent.ScheduledEndDate, createEvent.HasNCR, createEvent.LocationId, createEvent.Qty,
                createEvent.SerializeIndividually,
                createEvent.SerialNumbers, createEvent.CustomerLineNumbers);
        }
    }
}
