using MSR.Domain.Models;

namespace MSR.Domain.Events
{
    public class WorkOrderCreateEvent: BaseImportEvent
    {
        public PurchaseModel purchaseInfo { get; set; }
    }
}
