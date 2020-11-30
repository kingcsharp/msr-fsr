using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Events
{
    public class WorkOrderCreateEvent: BaseImportEvent
    {
        public PurchaseModel purchaseInfo { get; set; }

        /// <summary>
        /// Ordered list of serial numbers entered at purchase time
        /// (if any)
        /// </summary>
        public List<string> serialNumbers { get; set; }
    }
}
