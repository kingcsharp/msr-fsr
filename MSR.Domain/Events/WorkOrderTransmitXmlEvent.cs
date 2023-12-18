using System;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Events
{
    public class WorkOrderTransmitXmlEvent: BaseImportEvent
    {       
        public int WorkOrderId { get; set; }
    }
}
