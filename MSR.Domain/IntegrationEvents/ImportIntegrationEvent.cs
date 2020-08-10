using MSR.Domain.Commanding.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.IntegrationEvents
{
    public class ImportIntegrationEvent: BaseImportIntegrationEvent
    {
        public EnumMenuItem MenuItem { get; set; }
    }
}
