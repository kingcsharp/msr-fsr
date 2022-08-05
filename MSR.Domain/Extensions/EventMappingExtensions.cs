using System.Linq;
using MSR.Domain.Commands;
using MSR.Domain.Events;
using MSR.Domain.Models;

namespace MSR.Domain.Extensions
{
    public static class EventMappingExtensions
    {
        public static CreateWorkOrder ToCreateWorkOrderCommand(this WorkOrderCreateEvent createEvent)
        {
            return new CreateWorkOrder(createEvent.PurchaseId, createEvent.PurchaseOrderId, createEvent.ScheduledStartDate, createEvent.ScheduledEndDate,
                createEvent.HasNCR, createEvent.LocationId, createEvent.WorkOrderProducts.Select(i =>
                new WorkOrderProduct(i.ProductId, i.SerializeIndividually, i.SerialNumbers, i.CustomerLineNumbers, i.Qty, i.Price)).ToList());
        }
    }
}
